using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEntityEditor {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;

	public MonsterEditor(ProjectInfo project, MonsterInfo monster) : base(project, monster, monster, new MonsterDetails(monster)) {
	}
}