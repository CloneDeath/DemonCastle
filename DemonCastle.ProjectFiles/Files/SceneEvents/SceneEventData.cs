using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Files.SceneEvents;

public class SceneEventData {
	public string Name { get; set; } = string.Empty;
	public SceneEventConditionData When { get; set; } = new();
	public List<SceneEventActionData> Then { get; set; } = new();
}