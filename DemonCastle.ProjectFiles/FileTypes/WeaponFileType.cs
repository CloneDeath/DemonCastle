namespace DemonCastle.ProjectFiles.FileTypes;

public class WeaponFileType : IFileType {
	public string Name => "Weapon";
	public string Extension => ".dcw";
	public string Filter => "*.dcw; Demon Castle Weapon";
}