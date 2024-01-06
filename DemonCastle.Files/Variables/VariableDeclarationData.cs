using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Variables;

public class VariableDeclarationData {
	public Guid Id = Guid.NewGuid();
	public string Name = "Variable";
	public VariableType Type { get; protected set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum VariableType {
	Boolean,
	Integer,
	Float,
	String,
	Monster,
	Item,
	Vector2I
}


[AttributeUsage(AttributeTargets.Class)]
public class VariableTypeAttribute : Attribute {
	public VariableType VariableType { get; }

	public VariableTypeAttribute(VariableType variableType) {
		VariableType = variableType;
	}
}