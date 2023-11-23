namespace DemonCastle.ProjectFiles.FileTypes;

public class MonsterFileType : IFileType {
	public string Name => "Monster";
	public string Extension => ".dcm";
	public string Filter => "*.dcm; Demon Castle Monster";
}