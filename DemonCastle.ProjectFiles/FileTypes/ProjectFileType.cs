namespace DemonCastle.ProjectFiles.FileTypes;

public class ProjectFileType : IFileType {
	public string Name => "Project";
	public string Extension => ".dcp";
	public string Filter => "*.dcp; Demon Castle Project";
}