using System;

namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Item)]
public class ItemVariableDeclarationData : VariableDeclarationData {
	public Guid DefaultValue = Guid.Empty;
}