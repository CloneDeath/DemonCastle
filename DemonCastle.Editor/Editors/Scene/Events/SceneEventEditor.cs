using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events;

public partial class SceneEventEditor : PropertyCollection {
	public SceneEventEditor(IFileInfo file, ProjectInfo project, SceneEventInfo sceneEvent) {
		Name = nameof(SceneEventEditor);

		AddString("Name", sceneEvent, e => e.Name);
		AddChild(new SceneEventConditionEditor(sceneEvent.When));
		AddChild(new Label { Text = "Then" });
		AddChild(new SceneEventActionCollectionEditor(file, project, sceneEvent.Then));
	}
}