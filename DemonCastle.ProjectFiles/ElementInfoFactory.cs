using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Common;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles;

public static class ElementInfoFactory {
	public static readonly Dictionary<ElementType, Type> TypeMap = new() {
		{ ElementType.Label, typeof(LabelElementData) },
		{ ElementType.ColorRect, typeof(ColorRectElementData) },
		{ ElementType.Sprite, typeof(SpriteElementData) },
		{ ElementType.LevelView, typeof(LevelViewElementData) }
	};

	public static IElementInfo CreateInfo(IFileNavigator file, ElementData data) {
		return data.Type switch {
			ElementType.Label => new LabelElementInfo(file, (LabelElementData)data),
			ElementType.ColorRect => new ColorRectElementInfo(file, (ColorRectElementData)data),
			ElementType.Sprite => new SpriteElementInfo(file, (SpriteElementData)data),
			ElementType.LevelView => new LevelViewElementInfo(file, (LevelViewElementData)data),
			_ => throw new ArgumentOutOfRangeException(nameof(data), data.Type, null)
		};
	}

	public static ElementData CreateData(ElementType type) {
		return type switch {
			ElementType.Label => new LabelElementData {
				Name = "Label",
				Text = "Text"
			},
			ElementType.ColorRect => new ColorRectElementData {
				Name = "Color Rectangle",
				Region = new Region2I(0, 0, 16, 16)
			},
			ElementType.Sprite => new SpriteElementData{ Name = "Sprite" },
			ElementType.LevelView => new LevelViewElementData {
				Name = "Level View",
				Region = new Region2I(0, 0, 64, 64)
			},
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
}