namespace CsvJsonXmlProcessors.Models {
	public class FileInformation {
		public FileType Type { get; set; }
		public string Path { get; set; }

		public FileInformation(FileType type, string path) {
			Type = type;
			Path = path;
		}
	}

	public enum FileType {
		CSV,
		JSON,
		XML
	}
}
