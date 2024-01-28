using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class MonsterVariableDeclarationDetails : VariableDeclarationDetails {
	public MonsterVariableDeclarationDetails(ProjectInfo project, MonsterVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(MonsterVariableDeclarationDetails);

		AddMonsterReference("Default Value", variableDeclaration, v => v.DefaultValue, new EnumerableInfoWrapper<MonsterInfo>(project.Monsters));
	}
}