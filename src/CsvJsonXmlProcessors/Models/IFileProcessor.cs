using System.Collections.Generic;

namespace CsvJsonXmlProcessors.Models {
	public interface IFileProcessor {
		IEnumerable<string[]> ReadFromFile(FileInformation file);
		bool WriteToFile(List<User> users, FileInformation file);
	}
}
