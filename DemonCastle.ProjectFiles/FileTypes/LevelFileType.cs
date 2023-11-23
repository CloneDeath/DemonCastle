namespace DemonCastle.ProjectFiles.FileTypes;

public class LevelFileType : IFileType {
	public string Name => "Level";
	public string Extension => ".dcl";
	public string Filter => "*.dcl; Demon Castle Level";
}