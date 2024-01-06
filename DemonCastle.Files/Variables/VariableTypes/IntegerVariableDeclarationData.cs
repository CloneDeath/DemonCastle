namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Integer)]
public class IntegerVariableDeclarationData : VariableDeclarationData {
	public IntegerVariableDeclarationData() {
		Type = VariableType.Integer;
	}

	public int DefaultValue;
}