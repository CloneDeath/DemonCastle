namespace DemonCastle.ProjectFiles.FileTypes;

public class TtfFileType : IFileType {
	public string Name => "TrueType Font";
	public string Extension => ".ttf";
	public string Filter => "*.ttf; TrueType Font";
}