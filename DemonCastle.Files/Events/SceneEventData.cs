using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Conditions;

namespace DemonCastle.Files.Events;

public class SceneEventData {
	public string Name { get; set; } = string.Empty;
	public SceneEventConditionData When { get; set; } = new();
	public List<SceneActionData> Then { get; set; } = new();
}