using System;
using System.Collections.Generic;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles;

public static class ElementInfoFactory {
	public static readonly Dictionary<ElementType, Type> TypeMap = new() {
		{ ElementType.ColorRect, typeof(ColorRectElementData) },
		{ ElementType.HealthBar, typeof(HealthBarElementData) },
		{ ElementType.Label, typeof(LabelElementData) },
		{ ElementType.LevelView, typeof(LevelViewElementData) },
		{ ElementType.Sprite, typeof(SpriteElementData) }
	};

	public static IElementInfo CreateInfo(IFileNavigator file, ElementData data) {
		return data.Type switch {
			ElementType.ColorRect => new ColorRectElementInfo(file, (ColorRectElementData)data),
			ElementType.HealthBar => new HealthBarElementInfo(file, (HealthBarElementData)data),
			ElementType.Label => new LabelElementInfo(file, (LabelElementData)data),
			ElementType.LevelView => new LevelViewElementInfo(file, (LevelViewElementData)data),
			ElementType.Sprite => new SpriteElementInfo(file, (SpriteElementData)data),
			_ => throw new ArgumentOutOfRangeException(nameof(data), data.Type, null)
		};
	}

	public static ElementData CreateData(ElementType type) {
		return type switch {
			ElementType.ColorRect => new ColorRectElementData(),
			ElementType.HealthBar => new HealthBarElementData(),
			ElementType.Label => new LabelElementData(),
			ElementType.LevelView => new LevelViewElementData(),
			ElementType.Sprite => new SpriteElementData(),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
}