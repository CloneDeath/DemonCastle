using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class MonsterVariableDeclarationDetails : VariableDeclarationDetails {
	public MonsterVariableDeclarationDetails(ProjectResources resources, MonsterVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(MonsterVariableDeclarationDetails);

		AddMonsterReference("Default Value", variableDeclaration, v => v.DefaultValue, new EnumerableInfoWrapper<MonsterInfo>(resources.Monsters));
	}
}