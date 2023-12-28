using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventActionEditor : VBoxContainer {
	public SceneEventActionEditor(SceneEventActionInfo then) {
		Name = nameof(SceneEventActionEditor);
		AddChild(new Label{Text = "Then"});
	}
}