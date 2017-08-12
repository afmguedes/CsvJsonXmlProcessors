using System;
using System.Collections.Generic;
using System.IO;

namespace CsvJsonXmlProcessors.Models {
	public class CsvProcessor : IFileProcessor {
		public IEnumerable<string[]> ReadFromFile(FileInformation file) {
			List<string[]> data = null;

			try {
				using (var reader = new StreamReader(File.OpenRead(file.Path))) {
					data = new List<string[]>();

					var headers = reader.ReadLine(); // Ignore first line
					string line;

					while ((line = reader.ReadLine()) != null) {
						var values = line.Split(',');
						data.Add(values);
					}
				}
			} catch (Exception e) {
				Console.WriteLine($"Error reading data from CSV file:\r\n{e}");
			}

			return data;
		}

		public void WriteToFile(List<User> users, FileInformation file) {
			try {
				using (var writer = new StreamWriter(File.OpenWrite(file.Path))) {
					writer.WriteLine("User ID,First Name,Last Name,Username,User Type,Last Login Time");
					users.ForEach(u => writer.WriteLine($"{u.UserID},{u.FirstName},{u.LastName},{u.UserName},{u.Type.ToString()},{u.LastLoginTime:yyyy-MM-ddThh:mm:ss.ffffff}"));
				}
			} catch (Exception e) {
				Console.WriteLine($"Error writing data to CSV file:\r\n{e}");
			}
		}
	}
}
