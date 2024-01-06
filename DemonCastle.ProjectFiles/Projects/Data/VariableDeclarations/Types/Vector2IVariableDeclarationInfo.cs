using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class Vector2IVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly Vector2IVariableDeclarationData _data;

	public Vector2IVariableDeclarationInfo(IFileNavigator file, Vector2IVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public Vector2I DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}