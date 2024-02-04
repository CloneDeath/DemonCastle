using DemonCastle.Navigation;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class TextInfo {
	protected FileNavigator File { get; }

	public string FileName => File.FileName;

	public string Contents {
		get => File.LoadContent();
		set => File.SaveContent(value);
	}

	public TextInfo(FileNavigator file) {
		File = file;
	}
}