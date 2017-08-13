using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CsvJsonXmlProcessors.Services;

namespace CsvJsonXmlProcessors.Models {
	public class XmlProcessor : IFileProcessor {
		public IEnumerable<string[]> ReadFromFile(FileInformation file) {
			var fileContent = FileHandler.ReadFileContent(file.Path);

			if (string.IsNullOrEmpty(fileContent))
				return null;

			var usersElement = XDocument.Parse(fileContent).Element("users");

			var users = usersElement.Elements("user");

			var data = (from u in users
						where u.Element("userid") != null
							  && u.Element("firstname") != null
							  && u.Element("surname") != null
							  && u.Element("username") != null
							  && u.Element("type") != null
							  && u.Element("lastlogintime") != null
						select new[] {
								 u.Element("userid").Value,
								 u.Element("firstname").Value,
								 u.Element("surname").Value,
								 u.Element("username").Value,
								 u.Element("type").Value,
								 u.Element("lastlogintime").Value
							 }
			).ToList();

			return data;
		}

		public bool WriteToFile(List<User> users, FileInformation file) {
			var usersNode = new XElement("users");

			if (users.Count > 0)
				users.ForEach(u => usersNode.Add(new XElement("user",
								  new XElement("userid", u.UserID),
								  new XElement("firstname", u.FirstName),
								  new XElement("surname", u.LastName),
								  new XElement("username", u.UserName),
								  new XElement("type", u.Type.ToString()),
								  new XElement("lastlogintime", $"{u.LastLoginTime:yyyy-mm-ddThh:mm:ss.ffffff}")
							  )));

			var settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "    "
			};

			var data = new StringBuilder();

			using (var writer = XmlWriter.Create(data, settings)) {
				var doc = new XDocument(usersNode);
				doc.Save(writer);
			}

			return FileHandler.WriteContentToFile(file.Path, data.ToString());
		}
	}
}
