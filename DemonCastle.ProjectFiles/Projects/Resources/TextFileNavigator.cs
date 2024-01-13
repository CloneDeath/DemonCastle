using System.IO;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class TextFileNavigator : FileNavigator {
	public string Resource { get; set; }

	public TextFileNavigator(string filePath) : this(filePath, new ProjectResources()) { }

	public TextFileNavigator(string filePath, ProjectResources resources) : base(filePath, resources) {
		Resource = File.ReadAllText(filePath);
	}

	public override void Save() {
		File.WriteAllText(FilePath, Resource);
	}
}