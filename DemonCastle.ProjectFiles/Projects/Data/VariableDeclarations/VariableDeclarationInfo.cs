using System;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

public abstract class VariableDeclarationInfo : BaseInfo<VariableDeclarationData>, IListableInfo {
	protected VariableDeclarationInfo(IFileNavigator file, VariableDeclarationData data) : base(file, data) { }

	public Guid Id => Data.Id;
	public string ListLabel => Name;
	public VariableType Type => Data.Type;

	public string Name {
		get => Data.Name;
		set {
			if (SaveField(ref Data.Name, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}
}