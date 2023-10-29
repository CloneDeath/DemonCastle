using DemonCastle.Editor.Editors.Level.Area.Tiles;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;
using TileSelectorPanel = DemonCastle.Editor.Editors.Level.Area.TileTools.TileSelectorPanel;

namespace DemonCastle.Editor.Editors.Level.AreaOld; 

public partial class AreaEditor {
	public override Texture2D TabIcon => IconTextures.LevelIcon;
	public override string TabText { get; }
	
	protected HSplitContainer SplitContainer { get; }
	protected PropertyCollection Properties { get; }
	protected VSplitContainer ToolSplitContainer { get; }
	protected AreaTileEditor AreaTileEditor { get; }
	protected TileSelectorPanel TileSelector { get; }
	
	public AreaEditor(AreaInfo area) {
		Name = nameof(AreaEditor);
		TabText = $"Area - X:{area.AreaPosition.X}, Y:{area.AreaPosition.Y}";
		CustomMinimumSize = new Vector2I(400, 100) + area.Size * area.AreaSize * area.TileSize;
		
		AddChild(SplitContainer = new HSplitContainer {
			AnchorBottom = 1,
			AnchorRight = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = -5,
			OffsetBottom = -5
		});

		SplitContainer.AddChild(ToolSplitContainer = new VSplitContainer {
			AnchorBottom = 1,
			AnchorRight = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = -5,
			OffsetBottom = -5
		});
		
		ToolSplitContainer.AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", area, x => x.Name);
		Properties.AddVector2I("Position", area, x => x.AreaPosition);
		Properties.AddVector2I("Size", area, x => x.Size);

		ToolSplitContainer.AddChild(TileSelector = new TileSelectorPanel(area.TileSet));
		SplitContainer.AddChild(AreaTileEditor = new AreaTileEditor(area));
		AreaTileEditor.TileCellSelected += AreaTileEditorOnTileCellSelected;
		AreaTileEditor.TileCellCleared += AreaTileEditorOnTileCellCleared;
	}
}