using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Variables;

public class VariableDeclarationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public VariableDataType DataType { get; set; }
	public object? DefaultValue { get; set; }
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