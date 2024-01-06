using System;
using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

public class ItemVariableDeclarationInfo : VariableDeclarationInfo {
	private readonly ItemVariableDeclarationData _data;

	public ItemVariableDeclarationInfo(IFileNavigator file, ItemVariableDeclarationData data) : base(file, data) {
		_data = data;
	}

	public Guid DefaultValue {
		get => _data.DefaultValue;
		set => SaveField(ref _data.DefaultValue, value);
	}
}