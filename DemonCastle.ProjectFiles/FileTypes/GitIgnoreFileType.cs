namespace DemonCastle.ProjectFiles.FileTypes;

public class GitIgnoreFileType : IFileType {
	public string Name => "Git Ignore";
	public string Extension => ".gitignore";
	public string Filter => ".gitignore; Git Ignore";
}