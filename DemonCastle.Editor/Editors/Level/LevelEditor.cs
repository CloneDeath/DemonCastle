using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class LevelEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.LevelIcon;
	public override string TabText { get; }

	protected LevelInfo LevelInfo { get; }

	protected HSplitContainer SplitContainer { get; }
	protected LevelDetailsPanel DetailsPanel { get; }
	protected LevelView LevelView { get; }

	public LevelEditor(LevelInfo levelInfo) {
		Name = nameof(LevelEditor);
		TabText = levelInfo.FileName;
		CustomMinimumSize = new Vector2I(600, 400);

		LevelInfo = levelInfo;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		SplitContainer.AddChild(DetailsPanel = new LevelDetailsPanel(levelInfo));
		DetailsPanel.AreaAdded += DetailsPanelOnAreaAdded;
		SplitContainer.AddChild(LevelView = new LevelView(levelInfo));
	}

	private void DetailsPanelOnAreaAdded() {
		LevelView.Reload();
	}
}