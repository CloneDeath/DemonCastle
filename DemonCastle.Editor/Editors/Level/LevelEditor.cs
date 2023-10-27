using DemonCastle.Editor.Editors.Level.LevelOverview;
using DemonCastle.Editor.Editors.Level.LevelTiles;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class LevelEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.LevelIcon;
	public override string TabText { get; }

	protected LevelInfo Level { get; }

	protected VSplitContainer SplitContainer { get; }
	protected LevelOverviewEdit LevelOverview { get; }
	protected LevelTilesEdit LevelTiles { get; }

	public LevelEditor(LevelInfo level) {
		Name = nameof(LevelEditor);
		TabText = level.FileName;
		CustomMinimumSize = new Vector2I(600, 400);

		Level = level;

		AddChild(SplitContainer = new VSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(LevelOverview = new LevelOverviewEdit(level));
		SplitContainer.AddChild(LevelTiles = new LevelTilesEdit(level));
	}
}