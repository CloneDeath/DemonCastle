using System;

namespace DemonCastle.Files.Variables;

public class VariableDeclarationData {
	public Guid Id = Guid.NewGuid();
	public string Name = "Variable";
	public VariableType Type { get; protected set; }
}