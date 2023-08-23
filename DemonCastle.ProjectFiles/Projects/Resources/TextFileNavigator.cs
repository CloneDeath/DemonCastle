using System.IO;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public partial class TextFileNavigator : FileNavigator {
		public string Resource { get; }
		
		public TextFileNavigator(string filePath) : this(filePath, new ProjectResources()) { }

		public TextFileNavigator(string filePath, ProjectResources resources) : base(filePath, resources) {
			Resource = File.ReadAllText(filePath);
		}
	}
}