using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventConditionEditor : VBoxContainer {
	public SceneEventConditionEditor(SceneEventConditionInfo when) {
		Name = nameof(SceneEventConditionEditor);
		AddChild(new Label{Text = "When"});
	}
}