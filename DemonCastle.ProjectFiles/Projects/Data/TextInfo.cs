using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public partial class TextInfo {
		protected TextFileNavigator File { get; }

		public string FileName => File.FileName;
		public string Contents => File.Resource;

		public TextInfo(TextFileNavigator file) {
			File = file;
		}
	}
}