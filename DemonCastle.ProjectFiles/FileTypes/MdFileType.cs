namespace DemonCastle.ProjectFiles.FileTypes;

public class MdFileType : IFileType {
	public string Name => "Markdown";
	public string Extension => ".md";
	public string Filter => "*.md; Markdown";
}