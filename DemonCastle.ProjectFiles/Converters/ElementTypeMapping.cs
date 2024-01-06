using System;
using DemonCastle.Files;

namespace DemonCastle.ProjectFiles.Converters;

public static class ElementTypeMapping {
	public static Type GetDataType(ElementType elementType) => ElementInfoFactory.GetDataType(elementType);
	public static ElementType GetEnumValue(Type type) => ElementInfoFactory.GetElementType(type);
}