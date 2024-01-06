using System;
using System.Linq;
using System.Reflection;
using DemonCastle.Files;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Converters;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles;

public static class ElementInfoFactory {
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
		var dataType = GetDataType(type);
		if (dataType == null) throw new NotSupportedException();
		return (ElementData?)Activator.CreateInstance(dataType) ?? throw new NullReferenceException();
	}

	public static Type GetDataType(ElementType elementType) {
		var assembly = typeof(ElementTypeAttribute).Assembly;
		var types = assembly.GetTypes()
							.Where(type => type.GetCustomAttribute<ElementTypeAttribute>() != null);

		foreach (var type in types) {
			var attribute = type.GetCustomAttribute<ElementTypeAttribute>();
			if (attribute?.ElementType == elementType) {
				return type;
			}
		}
		throw new NotSupportedException($"Could not find a Data type for {elementType}");
	}

	public static ElementType GetElementType(Type type) {
		var attribute = type.GetCustomAttribute<ElementTypeAttribute>();
		return attribute?.ElementType ?? throw new NotSupportedException($"Type {type} does not have an ElementTypeAttribute.");
	}
}