using System;
using DemonCastle.Files;

namespace DemonCastle.ProjectFiles.Converters;

public class ElementTypeMapping : IEnumTypeMapping {
	public Type EnumType => typeof(ElementType);
	public Type BaseDataType => typeof(ElementData);
	public string NameOfEnumField => nameof(ElementData.Type);
	public Type GetDataType(Enum elementType) => ElementInfoFactory.GetDataType((ElementType)elementType);
	public Enum GetEnumValue(Type type) => ElementInfoFactory.GetElementType(type);
}

public interface IEnumTypeMapping {
	public Type EnumType { get; }
	public Type BaseDataType { get; }
	public string NameOfEnumField { get; }
	public Type GetDataType(Enum enumValue);
	public Enum GetEnumValue(Type type);
}