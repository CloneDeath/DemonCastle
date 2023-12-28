using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor;

public partial class ElementEditor : Container {
	public void LoadElement(IElementInfo element) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		var editor = GetEditor(element);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}

	private static Control GetEditor(IElementInfo element) {
		return element.Type switch {
			ElementType.Label => new ElementDetails(element),
			ElementType.ColorRect => new ElementDetails(element),
			_ => throw new ArgumentOutOfRangeException(nameof(element))
		};
	}
}