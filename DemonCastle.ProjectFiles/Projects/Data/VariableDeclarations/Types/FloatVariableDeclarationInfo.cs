using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class FloatVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly FloatVariableDeclarationData _data;

	public FloatVariableDeclarationInfo(IFileNavigator file, FloatVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public float DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}