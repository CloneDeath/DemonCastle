using DemonCastle.Editor.Editors.Scene.Events;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneItemEditor : Container {
	private readonly IFileInfo _file;
	private readonly ProjectInfo _project;

	public SceneItemEditor(ProjectInfo project, IFileInfo file) {
		_file = file;
		_project = project;
	}

	public void Clear() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}

	public void LoadElement(IElementInfo element) {
		Clear();

		var editor = ElementDetailsFactory.GetEditor(_file, _project, element);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}

	public void LoadEvent(SceneEventInfo sceneEvent) {
		Clear();

		var editor = new SceneEventEditor(_file, _project, sceneEvent);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}
}