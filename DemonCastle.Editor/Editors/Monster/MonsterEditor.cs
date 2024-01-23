using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEntityEditor<MonsterInfo, MonsterFile> {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;

	public MonsterEditor(ProjectInfo project, MonsterInfo monster) : base(project, monster, monster, new MonsterDetails(monster)) {
	}
}