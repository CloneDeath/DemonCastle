using System;

namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Monster)]
public class MonsterVariableDeclarationData : VariableDeclarationData {
	public Guid DefaultValue = Guid.Empty;
}