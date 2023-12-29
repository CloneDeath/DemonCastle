using System;
using DemonCastle.Editor.Editors.Scene.Elements.Editor.Types;
using DemonCastle.Editor.Editors.Scene.View.ElementTypes;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor;

public class EditorElementFactory {
	public static Control GetEditor(IFileInfo file, IElementInfo element) {
		return element.Type switch {
			ElementType.Label => new LabelElementDetails(file, (LabelElementInfo)element),
			ElementType.ColorRect => new ColorRectElementDetails((ColorRectElementInfo)element),
			ElementType.Sprite => new SpriteElementDetails(file, (SpriteElementInfo)element),
			ElementType.LevelView => new LevelViewElementDetails((LevelViewElementInfo)element),
			_ => throw new InvalidOperationException()
		};
	}

	public static Node GetView(IElementInfo element) {
		return element.Type switch {
			ElementType.Label => new LabelElementView((LabelElementInfo)element),
			ElementType.ColorRect => new ColorRectElementView((ColorRectElementInfo)element),
			ElementType.Sprite => new SpriteElementView((SpriteElementInfo)element),
			ElementType.LevelView => new LevelViewElementView((LevelViewElementInfo)element),
			_ => throw new ArgumentOutOfRangeException(nameof(element))
		};
	}

}