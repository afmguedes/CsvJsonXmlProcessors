using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvJsonXmlProcessors.Services;

namespace CsvJsonXmlProcessors.Models {
	public class CsvProcessor : IFileProcessor {
		public IEnumerable<string[]> ReadFromFile(FileInformation file) {
			var fileContent = FileHandler.ReadFileContent(file.Path);

			if (string.IsNullOrEmpty(fileContent))
				return null;

			var lines = fileContent.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1); // Ignore first line
			var data = lines.Select(line => line.Split(',')).ToList();

			return data;
		}

		public void WriteToFile(List<User> users, FileInformation file) {
			var data = new StringBuilder("User ID,First Name,Last Name,Username,User Type,Last Login Time\r\n");
			
			users.ForEach(u => data.AppendLine($"{u.UserID},{u.FirstName},{u.LastName},{u.UserName},{u.Type.ToString()},{u.LastLoginTime:yyyy-MM-ddThh:mm:ss.ffffff}"));

			FileHandler.WriteContentToFile(file.Path, data.ToString());
		}
	}
}
