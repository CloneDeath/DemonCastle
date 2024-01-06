namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Float)]
public class FloatVariableDeclarationData : VariableDeclarationData {
	public FloatVariableDeclarationData() {
		Type = VariableType.Float;
	}

	public float DefaultValue;
}