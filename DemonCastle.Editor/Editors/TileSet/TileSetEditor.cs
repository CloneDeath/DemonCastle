using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet;

public partial class TileSetEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.TileSet.Icon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }
	protected TileInfoCollectionEditor TileList { get; }
	protected TileEditor TileEditor { get; }

	public TileSetEditor(ProjectInfo project, TileSetInfo tileSet) {
		TabText = tileSet.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		var leftSide = new VBoxContainer {
			CustomMinimumSize = new Vector2(100, 0)
		};
		SplitContainer.AddChild(leftSide);
		{
			leftSide.AddChild(new TileSetDetails(tileSet));
			leftSide.AddChild(TileList = new TileInfoCollectionEditor(tileSet.TileSet) {
				SizeFlagsVertical = SizeFlags.ExpandFill
			});
			TileList.TileSelected += TileList_OnTileSelected;
		}
		SplitContainer.AddChild(TileEditor = new TileEditor(project, tileSet));
	}

	private void TileList_OnTileSelected(TileInfo? tile) => TileEditor.Load(tile);
}