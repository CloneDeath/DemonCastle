using System;
using System.Collections.Generic;
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

	public static ElementData CreateElementTypeData(ElementType type) {
		var dataType = new ElementTypeMapping().GetDataType(type);
		if (dataType == null) throw new NotSupportedException();
		return (ElementData?)Activator.CreateInstance(dataType) ?? throw new NullReferenceException();
	}

	public static IEnumerable<Type> GetTypesWith<TAttribute>() where TAttribute : Attribute {
		var assembly = typeof(TAttribute).Assembly;
		return assembly.GetTypes()
					   .Where(type => type.GetCustomAttribute<TAttribute>() != null);
	}
}