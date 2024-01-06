using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class BooleanVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly BooleanVariableDeclarationData _data;

	public BooleanVariableDeclarationInfo(IFileNavigator file, BooleanVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public bool DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}