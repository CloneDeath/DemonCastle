using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;
	public override string TabText { get; }

	public MonsterEditor(MonsterInfo monster) {
		TabText = monster.Name;
	}
}