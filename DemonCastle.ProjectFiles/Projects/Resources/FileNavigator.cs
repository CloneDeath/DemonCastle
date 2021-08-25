using System.IO;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public class FileNavigator : DirectoryNavigator {
		public string FilePath { get; }
		public string FileName => Path.GetFileName(FilePath);
		public string Extension => Path.GetExtension(FilePath);

		public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
		public FileNavigator(string filePath, ProjectResources resources) 
			: base(Path.GetDirectoryName(filePath), resources) {
			FilePath = filePath;
		}

		public TextInfo ToTextFile() => ProjectResources.GetText(FilePath);
	}
}