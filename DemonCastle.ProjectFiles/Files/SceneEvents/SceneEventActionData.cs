namespace DemonCastle.ProjectFiles.Files.SceneEvents;

public class SceneEventActionData {
	public SceneChangeActionData? Scene { get; set; }
	public string? SetCharacter { get; set; }
	public string? SetLevel { get; set; }
}

public class SceneChangeActionData {
	public string? Set { get; set; }
	public string? Push { get; set; }
	public int? Pop { get; set; }
}