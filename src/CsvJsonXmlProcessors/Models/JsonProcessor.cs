using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvJsonXmlProcessors.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace CsvJsonXmlProcessors.Models {
	public class JsonProcessor : IFileProcessor {
		public IEnumerable<string[]> ReadFromFile(FileInformation file) {
			var fileContent = FileHandler.ReadFileContent(file.Path);
			var array = JArray.Parse(fileContent);

			var data = new List<string[]>();

			// There would be an easier and cleaner way to convert JSON to a list of users,
			// but I chose to use this approach to keep consistency with my mapper class.

			foreach (var o in array.Children<JObject>()) {
				var properties = o.Properties().ToArray();
				var user = new string[6];

				for (var i = 0; i < properties.Count(); i++)
					user[i] = properties[i].First().ToString();

				data.Add(user);
			}

			return data;
		}

		public bool WriteToFile(List<User> users, FileInformation file) {
			var settings = new JsonSerializerSettings
			               {
				               ContractResolver = new CustomContractResolver(),
				               DateFormatString = "yyyy-MM-ddThh:mm:ss.ffffff"
			               };

			settings.Converters.Add(new StringEnumConverter());

			var data = JsonConvert.SerializeObject(users, Formatting.Indented, settings);

			return FileHandler.WriteContentToFile(file.Path, data);
		}
	}

	public class CustomContractResolver : DefaultContractResolver {
		private Dictionary<string, string> Mappings { get; }

		public CustomContractResolver() {
			Mappings = new Dictionary<string, string> {
				{"UserID", "user_id"},
				{"FirstName", "first_name"},
				{"LastName", "last_name"},
				{"UserName", "username"},
				{"Type", "user_type"},
				{"LastLoginTime", "last_login_time"}
			};
		}

		protected override string ResolvePropertyName(string propertyName) {
			string resolvedName;
			var resolved = Mappings.TryGetValue(propertyName, out resolvedName);
			return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
		}
	}
}
