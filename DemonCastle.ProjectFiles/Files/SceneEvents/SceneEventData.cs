using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Actions;

namespace DemonCastle.ProjectFiles.Files.SceneEvents;

public class SceneEventData {
	public string Name { get; set; } = string.Empty;
	public SceneEventConditionData When { get; set; } = new();
	public List<SceneActionData> Then { get; set; } = new();
}