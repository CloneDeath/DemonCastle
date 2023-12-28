using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventEditor : PropertyCollection {
	public SceneEventEditor(SceneEventInfo sceneEvent) {
		Name = nameof(SceneEventEditor);

		AddString("Name", sceneEvent, e => e.Name);
		AddChild(new SceneEventConditionEditor(sceneEvent.When));
		AddChild(new SceneEventActionEditor(sceneEvent.Then));
	}
}