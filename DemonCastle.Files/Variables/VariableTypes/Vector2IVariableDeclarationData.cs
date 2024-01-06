using Godot;

namespace DemonCastle.Files.Variables.VariableTypes;

[VariableType(VariableType.Vector2I)]
public class Vector2IVariableDeclarationData : VariableDeclarationData {
	public Vector2I DefaultValue = Vector2I.Zero;
}