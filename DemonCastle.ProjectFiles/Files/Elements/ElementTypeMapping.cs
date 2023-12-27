using System;
using System.Collections.Generic;
using System.Linq;

namespace DemonCastle.ProjectFiles.Files.Elements;

public static class ElementTypeMapping
{
	private static readonly Dictionary<ElementType, Type> TypeMap = new() {
		{ ElementType.Label, typeof(LabelElementData) },
		{ ElementType.ColorRect, typeof(ColorRectElementData) }
	};

	public static Type GetType(ElementType elementType) {
		return TypeMap[elementType];
	}

	public static ElementType GetTypeEnum(Type type) {
		var pair = TypeMap.FirstOrDefault(x => x.Value == type);
		if (pair.Equals(default(KeyValuePair<ElementType, Type>)))
		{
			throw new InvalidOperationException("Type not found in mapping.");
		}
		return pair.Key;
	}
}