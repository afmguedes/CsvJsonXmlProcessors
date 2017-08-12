using System;
using System.Collections.Generic;
using CsvJsonXmlProcessors.Models;

namespace CsvJsonXmlProcessors.Services {
	public static class Mapper {
		public static IEnumerable<User> MapToUserList(IEnumerable<string[]> fileData) {
			var userList = new List<User>();

			foreach (var row in fileData) {
				if (row.Length != 6) continue;

				int id;
				int.TryParse(row[0], out id);

				var type = UserType.Unknown;
				Enum.TryParse(row[4], true, out type);

				DateTime date;
				DateTime.TryParse(row[5], out date);

				var user = new User(id, row[1] ?? "", row[2] ?? "", row[3] ?? "", type, date);

				userList.Add(user);
			}

			return userList;
		}
	}
}
