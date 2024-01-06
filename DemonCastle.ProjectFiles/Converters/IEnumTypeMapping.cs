using System;

namespace DemonCastle.ProjectFiles.Converters;

public interface IEnumTypeMapping {
	public Type EnumType { get; }
	public Type BaseDataType { get; }
	public string NameOfEnumField { get; }
	public Type GetDataType(Enum enumValue);
	public Enum GetEnumValue(Type type);
}