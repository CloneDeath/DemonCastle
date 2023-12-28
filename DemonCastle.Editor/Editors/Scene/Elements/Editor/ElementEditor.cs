using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor;

public partial class ElementEditor : Container {
	private readonly IFileInfo _file;

	public ElementEditor(IFileInfo file) {
		_file = file;
	}

	public void Clear() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}

	public void LoadElement(IElementInfo element) {
		Clear();

		var editor = EditorElementFactory.GetEditor(_file, element);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}
}