namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.String)]
public class StringVariableDeclarationData : VariableDeclarationData {
	public StringVariableDeclarationData() {
		Type = VariableType.String;
	}

	public string DefaultValue = string.Empty;
}