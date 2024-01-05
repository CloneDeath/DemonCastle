using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;

namespace DemonCastle.ProjectFiles.Converters;

public static class ElementTypeMapping {
	public static Type GetType(ElementType elementType) {
		return ElementInfoFactory.TypeMap[elementType];
	}

	public static ElementType GetTypeEnum(Type type) {
		var pair = ElementInfoFactory.TypeMap.FirstOrDefault(x => x.Value == type);
		if (pair.Equals(default(KeyValuePair<ElementType, Type>)))
		{
			throw new InvalidOperationException("Type not found in mapping.");
		}
		return pair.Key;
	}
}