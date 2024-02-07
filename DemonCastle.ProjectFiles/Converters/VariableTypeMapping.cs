using System;
using System.Linq;
using System.Reflection;
using DemonCastle.Files.Variables;

namespace DemonCastle.ProjectFiles.Converters;

public class VariableTypeMapping : IEnumTypeMapping {
	public Type EnumType => typeof(VariableType);
	public Type BaseDataType { get; }
	public string NameOfEnumField { get; }

	public VariableTypeMapping(Type baseType, string nameOfEnumField) {
		BaseDataType = baseType;
		NameOfEnumField = nameOfEnumField;
	}

	public Type GetDataType(Enum variableType) {
		var types = InfoFactory.GetTypesWith<VariableTypeAttribute>()
							   .Where(t => t.IsAssignableTo(BaseDataType));
		foreach (var type in types) {
			var attribute = type.GetCustomAttribute<VariableTypeAttribute>();
			if (attribute?.VariableType == (VariableType)variableType) {
				return type;
			}
		}

		throw new NotSupportedException($"Could not find a Data type for {variableType}");
	}

	public Enum GetEnumValue(Type type) {
		var attribute = type.GetCustomAttribute<VariableTypeAttribute>();
		return attribute?.VariableType ?? throw new NotSupportedException($"Type {type} does not have an {nameof(VariableTypeAttribute)}.");
	}
}