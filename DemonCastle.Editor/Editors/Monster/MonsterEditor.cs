using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Monster.Icon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }
	protected TabContainer RightArea { get; }

	public MonsterEditor(MonsterInfo monster) {
		TabText = monster.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsPreset(LayoutPreset.FullRect);

		SplitContainer.AddChild(new MonsterDetails(monster));
		SplitContainer.AddChild(RightArea = new TabContainer());

		RightArea.AddChild(new Control());
		RightArea.SetTabTitle(0, "Animations");
		RightArea.AddChild(new Control());
		RightArea.SetTabTitle(1, "States");
	}
}