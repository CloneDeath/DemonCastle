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
				() => Guid.Parse((string)(variableDeclaration.DefaultValue ?? Guid.Empty.ToString())),
				value => variableDeclaration.DefaultValue = value.ToString()),
			project.Items) {
			DisplayName = "Default Value"
		});
	}
}