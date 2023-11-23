namespace DemonCastle.ProjectFiles.FileTypes;

public class CharacterFileType : IFileType {
	public string Name => "Character";
	public string Extension => ".dcc";
	public string Filter => "*.dcc; Demon Castle Character";
}