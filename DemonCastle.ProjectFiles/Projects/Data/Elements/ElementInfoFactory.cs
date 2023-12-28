using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public static class ElementInfoFactory {
	public static IElementInfo CreateInfo(IFileNavigator file, ElementData data) {
		return data.Type switch {
			ElementType.Label => new LabelElementInfo(file, (LabelElementData)data),
			ElementType.ColorRect => new ColorRectElementInfo(file, (ColorRectElementData)data),
			_ => throw new ArgumentOutOfRangeException(nameof(data), data.Type, null)
		};
	}

	public static ElementData CreateData(ElementType type) {
		return type switch {
			ElementType.Label => new LabelElementData { Name = "Label" },
			ElementType.ColorRect => new ColorRectElementData { Name = "Color Rectangle" },
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
}