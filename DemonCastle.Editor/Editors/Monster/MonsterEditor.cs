using DemonCastle.Editor.Editors.BaseEntity;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEntityEditor<MonsterInfo, MonsterFile> {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;

	public MonsterEditor(MonsterInfo monster) : base(monster, new MonsterDetails(monster)) {
	}
}