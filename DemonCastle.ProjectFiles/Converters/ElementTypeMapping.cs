using System;
using System.Reflection;
using DemonCastle.Files;

namespace DemonCastle.ProjectFiles.Converters;

public class ElementTypeMapping : IEnumTypeMapping {
	public Type EnumType => typeof(ElementType);
	public Type BaseDataType => typeof(ElementData);
	public string NameOfEnumField => nameof(ElementData.Type);

	public Type GetDataType(Enum elementType) {
		var types = ElementInfoFactory.GetTypesWith<ElementTypeAttribute>();
		foreach (var type in types) {
			var attribute = type.GetCustomAttribute<ElementTypeAttribute>();
			if (attribute?.ElementType == (ElementType)elementType) {
				return type;
			}
		}
		throw new NotSupportedException($"Could not find a Data type for {elementType}");
	}

	public Enum GetEnumValue(Type type) {
		var attribute = type.GetCustomAttribute<ElementTypeAttribute>();
		return attribute?.ElementType ?? throw new NotSupportedException($"Type {type} does not have an ElementTypeAttribute.");
	}
}

public interface IEnumTypeMapping {
	public Type EnumType { get; }
	public Type BaseDataType { get; }
	public string NameOfEnumField { get; }
	public Type GetDataType(Enum enumValue);
	public Enum GetEnumValue(Type type);
}