namespace DemonCastle.ProjectFiles.FileTypes;

public class TextFileType : IFileType {
	public string Name => "Text";
	public string Extension => ".txt";
	public string Filter => "*.txt; Text";
}