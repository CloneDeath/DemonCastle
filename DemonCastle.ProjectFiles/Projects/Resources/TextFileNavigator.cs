using System.IO;
using DemonCastle.Navigation;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class TextFileNavigator : FileNavigator {
	public string Resource { get; set; }

	public TextFileNavigator(string filePath) : base(filePath) {
		Resource = File.ReadAllText(filePath);
	}

	public override void Save() {
		File.WriteAllText(FilePath, Resource);
	}
}