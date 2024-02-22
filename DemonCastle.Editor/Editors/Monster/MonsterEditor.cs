using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEntityEditor {
	public MonsterEditor(ProjectResources resources, ProjectInfo project, MonsterInfo monster) : base(resources, project, monster, monster, new MonsterDetails(monster)) {
	}
}