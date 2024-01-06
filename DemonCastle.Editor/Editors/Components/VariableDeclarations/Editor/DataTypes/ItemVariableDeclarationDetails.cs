using System;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class ItemVariableDeclarationDetails : VariableDeclarationDetails {
	public ItemVariableDeclarationDetails(ProjectInfo project, VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(ItemVariableDeclarationDetails);

		AddChild(new ItemReferenceProperty(new CallbackBinding<Guid>(
			() => (Guid)(variableDeclaration.DefaultValue ?? Guid.Empty),
			value => variableDeclaration.DefaultValue = value),
			project.Items) {
			DisplayName = "Default Value"
		});
	}
}