using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvJsonXmlProcessors.Models;
using Moq;
using Xunit;

namespace CsvJsonXmlProcessors_Tests {
	public class ProcessorTests {
		[Fact]
		public void FactoryCreatesCorrectProcessor() {
			var processorFactory = new Mock<ProcessorFactory>().Object;

			var csvProcessor = processorFactory.CreateFileProcessor(FileType.CSV);
			var jsonProcessor = processorFactory.CreateFileProcessor(FileType.JSON);
			var xmlProcessor = processorFactory.CreateFileProcessor(FileType.XML);

			Assert.IsType<CsvProcessor>(csvProcessor);
			Assert.IsType<JsonProcessor>(jsonProcessor);
			Assert.IsType<XmlProcessor>(xmlProcessor);
		}

		[Fact]
		public void ProcessorCanReadCsvData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var csvProcessor = processorFactory.CreateFileProcessor(FileType.CSV);
			var csvReadTestFile = new FileInformation(FileType.CSV, $"{Directory.GetCurrentDirectory()}\\usersReadTest.csv");

			var userList = csvProcessor.ReadFromFile(csvReadTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 2);
		}

		[Fact]
		public void ProcessorCanWriteCsvData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var csvProcessor = processorFactory.CreateFileProcessor(FileType.CSV);
			var csvWriteTestFile = new FileInformation(FileType.CSV, $"{Directory.GetCurrentDirectory()}\\usersWriteTest.csv");
			var userList = new List<User>() {
				new User(1, "Andre", "Guedes", "afmguedes", UserType.Employee, new DateTime(2017, 8, 12)),
				new User(2, "Graziano", "Cava", "grax", UserType.Employee, new DateTime(2017, 8, 12))
			};

			var result = csvProcessor.WriteToFile(userList, csvWriteTestFile);

			Assert.True(result);
		}

		[Fact]
		public void ProcessorCanReadJsonData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var jsonProcessor = processorFactory.CreateFileProcessor(FileType.JSON);
			var jsonReadTestFile = new FileInformation(FileType.JSON, $"{Directory.GetCurrentDirectory()}\\usersReadTest.json");

			var userList = jsonProcessor.ReadFromFile(jsonReadTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 5);
		}

		[Fact]
		public void ProcessorCanWriteJsonData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var jsonProcessor = processorFactory.CreateFileProcessor(FileType.JSON);
			var jsonWriteTestFile = new FileInformation(FileType.JSON, $"{Directory.GetCurrentDirectory()}\\usersWriteTest.json");
			var userList = new List<User>() {
				                                new User(1, "Andre", "Guedes", "afmguedes", UserType.Employee, new DateTime(2017, 8, 12)),
				                                new User(2, "Graziano", "Cava", "grax", UserType.Employee, new DateTime(2017, 8, 12))
			                                };

			var result = jsonProcessor.WriteToFile(userList, jsonWriteTestFile);

			Assert.True(result);
		}

		[Fact]
		public void ProcessorCanReadXmlData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var xmlProcessor = processorFactory.CreateFileProcessor(FileType.XML);
			var xmlReadTestFile = new FileInformation(FileType.XML, $"{Directory.GetCurrentDirectory()}\\usersReadTest.xml");

			var userList = xmlProcessor.ReadFromFile(xmlReadTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 3);
		}

		[Fact]
		public void ProcessorCanWriteXmlData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var xmlProcessor = processorFactory.CreateFileProcessor(FileType.XML);
			var xmlWriteTestFile = new FileInformation(FileType.XML, $"{Directory.GetCurrentDirectory()}\\usersWriteTest.xml");
			var userList = new List<User>() {
				                                new User(1, "Andre", "Guedes", "afmguedes", UserType.Employee, new DateTime(2017, 8, 12)),
				                                new User(2, "Graziano", "Cava", "grax", UserType.Employee, new DateTime(2017, 8, 12))
			                                };

			var result = xmlProcessor.WriteToFile(userList, xmlWriteTestFile);

			Assert.True(result);
		}
	}
}
