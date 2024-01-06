using System;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

public class VariableDeclarationInfo : BaseInfo<VariableDeclarationData>, IListableInfo {
	public VariableDeclarationInfo(IFileNavigator file, VariableDeclarationData data) : base(file, data) { }

	public Guid Id => Data.Id;
	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			if (SaveField(ref Data.Name, value)) {
				OnPropertyChanged(ListLabel);
			}
		}
	}

	public VariableDataType DataType {
		get => Data.DataType;
		set => SaveField(ref Data.DataType, value);
	}

	public object? DefaultValue {
		get => Data.DefaultValue;
		set => SaveField(ref Data.DefaultValue, value);
	}
}