using System;
using DemonCastle.Editor.Editors.Scene.Elements.Editor.Types;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor;

public partial class ElementEditor : Container {
	private readonly IFileInfo _file;

	public ElementEditor(IFileInfo file) {
		_file = file;
	}

	public void LoadElement(IElementInfo element) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		var editor = GetEditor(_file, element);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}

	private static Control GetEditor(IFileInfo file, IElementInfo element) {
		return element.Type switch {
			ElementType.Label => new LabelElementDetails(file, (LabelElementInfo)element),
			ElementType.ColorRect => new ElementDetails(element),
			_ => throw new ArgumentOutOfRangeException(nameof(element))
		};
	}
}