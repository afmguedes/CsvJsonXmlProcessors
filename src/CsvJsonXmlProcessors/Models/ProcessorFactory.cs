namespace CsvJsonXmlProcessors.Models {
	public class ProcessorFactory {
		public IFileProcessor CreateFileProcessor(FileType type) {

			IFileProcessor fileProcessor = null;

			switch (type) {
				case FileType.CSV:
					fileProcessor = new CsvProcessor();
					break;
				case FileType.JSON:
					fileProcessor = new JsonProcessor();
					break;
				case FileType.XML:
					fileProcessor = new XmlProcessor();
					break;
			}

			return fileProcessor;
		}
	}
}
