using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvJsonXmlProcessors.Models;
using CsvJsonXmlProcessors.Services;
using Xunit;

namespace CsvJsonXmlProcessors_Tests {
	public class ServicesTests {
		[Fact]
		public void IgnoreNullOrEmptyParsedData() {
			var parsedData = new List<string[]> { new string[] { } };

			var userList = Mapper.MapToUserList(parsedData).ToList();

			Assert.Empty(userList);
		}

		[Fact]
		public void IgnoreParsedDataWithIncorrectNumberOfProperties() {
			var parsedData = new List<string[]> { new[] { "1", "Andre", "Guedes", "afmguedes", "Employee" } };

			var userList = Mapper.MapToUserList(parsedData).ToList();

			Assert.Empty(userList);
		}

		[Fact]
		public void CreateNewUserBasedOnEmptyParsedValues() {
			var parsedData = new List<string[]> { new[] { "", "", "", "", "", "" } };

			var userList = Mapper.MapToUserList(parsedData).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList[0].UserID, 0);
			Assert.Equal(userList[0].FirstName, string.Empty);
			Assert.Equal(userList[0].Type, UserType.Unknown);
		}

		[Fact]
		public void CanMapValidParsedDataToNewUser() {
			var parsedData = new List<string[]> { new[] { "1", "Andre", "Guedes", "afmguedes", "Employee", "12-08-2017 00:00:00" } };

			var userList = Mapper.MapToUserList(parsedData).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList[0].UserID, 1);
		}

		[Fact]
		public void CanMapValidParsedDataToListOfUsers() {
			var parsedData = new List<string[]> {
				new[] { "1", "Andre", "Guedes", "afmguedes", "Manager", "12-08-2017 00:00:00" },
				new[] { "2", "Graziano", "Cava", "grax", "Employee", "12-08-2017 00:00:00" }
			};

			var userList = Mapper.MapToUserList(parsedData).ToList();

			Assert.NotEmpty(userList);
			Assert.Equal(userList[0].UserID, 1);
		}
	}
}
