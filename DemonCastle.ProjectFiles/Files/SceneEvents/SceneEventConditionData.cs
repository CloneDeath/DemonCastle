using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files.SceneEvents;

public class SceneEventConditionData {
	public SceneEventButtonCondition? Button { get; set; }
}

public class SceneEventButtonCondition {
	public ButtonAction? IsPressed { get; set; }
	public ButtonAction? IsReleased { get; set; }
	public ButtonAction? IsDown { get; set; }
	public ButtonAction? IsUp { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum ButtonAction {
	Up,
	Down,
	Left,
	Right,
	A,
	B,
	X,
	Y,
	Start,
	Select
}