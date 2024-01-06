using System;
using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class MonsterVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly MonsterVariableDeclarationData _data;

	public MonsterVariableDeclarationInfo(IFileNavigator file, MonsterVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public Guid DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}