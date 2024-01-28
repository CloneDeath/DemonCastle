using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class ItemVariableDeclarationDetails : VariableDeclarationDetails {
	public ItemVariableDeclarationDetails(ProjectInfo project, ItemVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(ItemVariableDeclarationDetails);

		AddItemReference("Default Value", variableDeclaration, v => v.DefaultValue, new EnumerableInfoWrapper<ItemInfo>(project.Items));
	}
}