using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.SceneEvents;

public class SceneEventConditionData {
	//public SceneEventConditionData[]? And { get; set; }
	//public SceneEventConditionData[]? Or { get; set; }
	public KeyState? AnyInput { get; set; }
	public InputConditionData? Input { get; set; }
	public SceneChangeEvent? ThisScene { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum KeyState {
	Pressed,
	Released,
	Down,
	Up
}

public class InputConditionData {
	public KeyState State { get; set; }
	public PlayerAction Action { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum PlayerAction {
	Up,
	Down,
	Left,
	Right,
	Jump,
	Attack
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SceneChangeEvent {
	Enter,
	Exit
}