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
		public void CanReadCsvData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var csvProcessor = processorFactory.CreateFileProcessor(FileType.CSV);
			var csvTestFile = new FileInformation(FileType.CSV, $"{Directory.GetCurrentDirectory()}\\usersTest.csv");

			var userList = csvProcessor.ReadFromFile(csvTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 2);
		}



		[Fact]
		public void CanReadJsonData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var jsonProcessor = processorFactory.CreateFileProcessor(FileType.JSON);
			var jsonTestFile = new FileInformation(FileType.JSON, $"{Directory.GetCurrentDirectory()}\\usersTest.json");

			var userList = jsonProcessor.ReadFromFile(jsonTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 5);
		}




		[Fact]
		public void CanReadXmlData() {
			var processorFactory = new Mock<ProcessorFactory>().Object;
			var xmlProcessor = processorFactory.CreateFileProcessor(FileType.XML);
			var xmlTestFile = new FileInformation(FileType.XML, $"{Directory.GetCurrentDirectory()}\\usersTest.xml");

			var userList = xmlProcessor.ReadFromFile(xmlTestFile).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList.Count, 3);
		}
	}
}
