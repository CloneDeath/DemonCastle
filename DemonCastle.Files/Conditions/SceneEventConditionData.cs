using DemonCastle.Files.Conditions.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Conditions;

public class SceneEventConditionData {
	//public SceneEventConditionData[]? And { get; set; }
	//public SceneEventConditionData[]? Or { get; set; }
	public KeyState? AnyInput { get; set; }
	public InputConditionData? Input { get; set; }
	public SceneChangeEvent? ThisScene { get; set; }

	public void Clear() {
		AnyInput = null;
		Input = null;
		ThisScene = null;
	}
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
	Attack,
	Accept,
	Cancel
}