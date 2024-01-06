namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Boolean)]
public class BooleanVariableDeclarationData : VariableDeclarationData {
	public BooleanVariableDeclarationData() {
		Type = VariableType.Boolean;
	}

	public bool DefaultValue;
}