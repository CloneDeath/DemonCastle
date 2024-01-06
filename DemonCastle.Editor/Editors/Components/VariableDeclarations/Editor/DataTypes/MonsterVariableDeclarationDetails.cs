using System;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class MonsterVariableDeclarationDetails : VariableDeclarationDetails {
	public MonsterVariableDeclarationDetails(ProjectInfo project, VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(MonsterVariableDeclarationDetails);

		AddChild(new MonsterReferenceProperty(new CallbackBinding<Guid>(
			() => (Guid)(variableDeclaration.DefaultValue ?? Guid.Empty),
			value => variableDeclaration.DefaultValue = value),
			project.Monsters) {
			DisplayName = "Default Value"
		});
	}
}