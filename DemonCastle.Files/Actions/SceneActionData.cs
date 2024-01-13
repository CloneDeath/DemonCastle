using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Actions;

public class SceneActionData {
	public GameAction? Game { get; set; }
	public SceneChangeActionData? Scene { get; set; }
	public string? SetCharacter { get; set; }
	public string? SetLevel { get; set; }

	public void Clear() {
		Game = null;
		Scene = null;
		SetCharacter = null;
		SetLevel = null;
	}
}

[JsonConverter(typeof(StringEnumConverter))]
public enum GameAction {
	Quit,
	Restart
}

public class SceneChangeActionData {
	public string? Set { get; set; }
	public string? Push { get; set; }
	public int? Pop { get; set; }
}