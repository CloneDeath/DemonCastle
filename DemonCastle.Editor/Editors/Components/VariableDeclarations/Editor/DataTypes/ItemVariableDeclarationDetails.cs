using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class ItemVariableDeclarationDetails : VariableDeclarationDetails {
	public ItemVariableDeclarationDetails(ProjectResources resources, ItemVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(ItemVariableDeclarationDetails);

		AddItemReference("Default Value", variableDeclaration, v => v.DefaultValue, new EnumerableInfoWrapper<ItemInfo>(resources.Items));
	}
}