using DemonCastle.ProjectFiles.Projects.Data;
using Godot;
using Path = System.IO.Path;

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

		public TextInfo ToTextInfo() => ProjectResources.GetText(FilePath);
		public Texture ToTexture() => ProjectResources.GetTexture(FilePath);
	}
}