using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEntityEditor {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;

	public MonsterEditor(ProjectResources resources, ProjectInfo project, MonsterInfo monster) : base(resources, project, monster, monster, new MonsterDetails(monster)) {
	}
}