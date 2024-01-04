namespace DemonCastle.ProjectFiles.FileTypes;

public class ItemFileType : IFileType {
	public string Name => "Item";
	public string Extension => ".dci";
	public string Filter => "*.dci; Demon Castle Item";
}