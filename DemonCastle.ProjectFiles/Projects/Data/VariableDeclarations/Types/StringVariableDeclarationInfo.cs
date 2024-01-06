using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class StringVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly StringVariableDeclarationData _data;

	public StringVariableDeclarationInfo(IFileNavigator file, StringVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public string DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}