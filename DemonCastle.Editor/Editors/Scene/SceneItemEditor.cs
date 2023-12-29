using DemonCastle.Editor.Editors.Scene.Events.Editor;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneItemEditor : Container {
	private readonly IFileInfo _file;

	public SceneItemEditor(IFileInfo file) {
		_file = file;
	}

	public void Clear() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}

	public void LoadElement(IElementInfo element) {
		Clear();

		var editor = ElementDetailsFactory.GetEditor(_file, element);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}

	public void LoadEvent(SceneEventInfo sceneEvent) {
		Clear();

		var editor = new SceneEventEditor(_file, sceneEvent);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}
}