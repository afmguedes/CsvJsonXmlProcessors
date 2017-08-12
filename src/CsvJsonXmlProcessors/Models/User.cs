using System;

namespace CsvJsonXmlProcessors.Models {
	public class User {
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public UserType Type { get; set; }
		public DateTime LastLoginTime { get; set; }

		public User(int userId, string firstName, string lastName, string userName, UserType type, DateTime lastLoginTime) {
			UserID = userId;
			FirstName = firstName;
			LastName = lastName;
			UserName = userName;
			Type = type;
			LastLoginTime = lastLoginTime;
		}
	}

	public enum UserType {
		Employee,
		Manager,
		Unknown
	}
}
