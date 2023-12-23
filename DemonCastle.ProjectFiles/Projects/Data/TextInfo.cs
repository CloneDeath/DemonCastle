using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class TextInfo {
	protected TextFileNavigator File { get; }

	public string FileName => File.FileName;
	public string Contents {
		get => File.Resource;
		set { File.Resource = value; File.Save(); }
	}

	public TextInfo(TextFileNavigator file) {
		File = file;
	}

	public void Save() => File.Save();
}