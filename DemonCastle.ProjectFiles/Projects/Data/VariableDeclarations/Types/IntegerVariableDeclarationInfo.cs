using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class IntegerVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly IntegerVariableDeclarationData _data;

	public IntegerVariableDeclarationInfo(IFileNavigator file, IntegerVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public int DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}