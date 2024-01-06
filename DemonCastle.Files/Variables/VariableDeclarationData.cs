using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Variables;

public class VariableDeclarationData {
	public Guid Id = Guid.NewGuid();
	public string Name = "Variable";
	public VariableDataType DataType;
	public object? DefaultValue;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum VariableDataType {
	Boolean,
	Integer,
	Decimal,
	String,
	Monster,
	Item
}