using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CsvJsonXmlProcessors.Models {
	public class XmlProcessor : IFileProcessor {
		public IEnumerable<string[]> ReadFromFile(FileInformation file) {
			List<string[]> data = null;

			try {
				using (var reader = XmlReader.Create(File.OpenRead(file.Path))) {
					data = new List<string[]>();

					var document = XDocument.Load(reader);
					var usersElement = document.Element("users");

					if (usersElement != null) {
						var users = usersElement.Elements("user");

						data = (from u in users
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
					}
				}
			} catch (Exception e) {
				Console.WriteLine($"Error reading data from XML file:\r\n{e}");
			}

			return data;
		}

		public void WriteToFile(List<User> users, FileInformation file) {
			try {
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

				using (var writer = XmlWriter.Create(File.OpenWrite(file.Path), settings)) {
					var doc = new XDocument(usersNode);
					doc.Save(writer);
				}
			} catch (Exception e) {
				Console.WriteLine($"Error writing data to XML file:\r\n{e}");
			}
		}
	}
}
