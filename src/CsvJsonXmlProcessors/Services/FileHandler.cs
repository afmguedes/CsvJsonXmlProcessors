using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvJsonXmlProcessors.Services {
	public static class FileHandler {
		public static string ReadFileContent(string filePath) {
			var content = string.Empty;

			try {
				var fileStream = File.OpenRead(filePath);
				
				using (var reader = new StreamReader(fileStream)) {
					content = reader.ReadToEnd();
				}

			} catch (Exception e) {
				Console.WriteLine($"Error attempting to read from file: {filePath} with error:\r\n{e}");
			}

			return content;
		}

		public static bool WriteContentToFile(string filePath, string content) {
			try {
				var fileStream = File.OpenWrite(filePath);

				using (var writer = new StreamWriter(fileStream)) {
					writer.Write(content);
				}

				return true;
			} catch (Exception e) {
				Console.WriteLine($"Error attempting to write to file: {filePath} with error:\r\n{e}");
			}

			return false;
		}
	}
}
