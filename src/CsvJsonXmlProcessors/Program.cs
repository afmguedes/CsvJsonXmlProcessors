using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvJsonXmlProcessors.Models;
using CsvJsonXmlProcessors.Services;

namespace CsvJsonXmlProcessors {
	public class Program {
		public static void Main(string[] args) {

			var dataFolderPath = $"{Directory.GetCurrentDirectory()}\\Data\\";

			var originalCsvFile = new FileInformation(FileType.CSV, $"{dataFolderPath}users.csv");
			var resultCsvFile = new FileInformation(FileType.CSV, $"{dataFolderPath}usersResult.csv");

			var originalJsonFile = new FileInformation(FileType.JSON, $"{dataFolderPath}users.json");
			var resultJsonFile = new FileInformation(FileType.JSON, $"{dataFolderPath}usersResult.json");

			var originalXmlFile = new FileInformation(FileType.XML, $"{dataFolderPath}users.xml");
			var resultXmlFile = new FileInformation(FileType.XML, $"{dataFolderPath}usersResult.xml");

			var factory = new ProcessorFactory();
			var csvProcessor = factory.CreateFileProcessor(FileType.CSV);
			var jsonProcessor = factory.CreateFileProcessor(FileType.JSON);
			var xmlProcessor = factory.CreateFileProcessor(FileType.XML);

			var csvPayload = csvProcessor.ReadFromFile(originalCsvFile);
			var jsonPayload = jsonProcessor.ReadFromFile(originalJsonFile);
			var xmlPayload = xmlProcessor.ReadFromFile(originalXmlFile);

			var csvUserList = Mapper.MapToUserList(csvPayload);
			var jsonUserList = Mapper.MapToUserList(jsonPayload);
			var xmlUserList = Mapper.MapToUserList(xmlPayload);

			var resultUserList = new List<User>();
			resultUserList.AddRange(csvUserList);
			resultUserList.AddRange(xmlUserList);
			resultUserList.AddRange(jsonUserList);

			if (resultUserList.Count > 0)
				resultUserList = resultUserList.OrderBy(u => u.UserID).ToList();

			csvProcessor.WriteToFile(resultUserList, resultCsvFile);
			jsonProcessor.WriteToFile(resultUserList, resultJsonFile);
			xmlProcessor.WriteToFile(resultUserList, resultXmlFile);

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}
