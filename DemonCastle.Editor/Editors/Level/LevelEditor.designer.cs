using DemonCastle.Editor.Editors.Level.Area;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level; 

public partial class LevelEditor {
	protected LevelInfo LevelInfo { get; }
	
	protected HSplitContainer SplitContainer { get; }
	protected Control LeftPanel { get; }
	
	protected PropertyCollection Properties { get; }
	protected Button AddAreaButton { get; }
	protected Button AddTileButton { get; }
	protected Button EditTileButton { get; }
	protected Button DeleteTileButton { get; }
	protected TileSelectorPanel TileSelector { get; }
	
	protected AreaEditor AreaEditor { get; }

	public LevelEditor(LevelInfo levelInfo) {
		Name = $"Level - {levelInfo.FileName}";
		CustomMinimumSize = new Vector2I(600, 400);
		
		LevelInfo = levelInfo;
		
		AddChild(SplitContainer = new HSplitContainer {
			AnchorBottom = 1,
			AnchorRight = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = -5,
			OffsetBottom = -5
		});
		SplitContainer.AddChild(LeftPanel = new VBoxContainer());
		{
			LeftPanel.AddChild(Properties = new PropertyCollection());
			Properties.AddString("Name", levelInfo, x => x.Name);
			Properties.AddVector2I("Tile Size", levelInfo, x => x.TileSize);
			Properties.AddVector2I("Area Size", levelInfo, x => x.AreaSize);
			
			LeftPanel.AddChild(AddAreaButton = new Button { Text = "Add Area" });
			AddAreaButton.Pressed += AddAreaButtonOnPressed;
			
			LeftPanel.AddChild(AddTileButton = new Button { Text = "Add Tile" });
			AddTileButton.Pressed += AddTileButtonOnPressed; 
			
			LeftPanel.AddChild(EditTileButton = new Button { Text = "Edit Tile" });
			EditTileButton.Pressed += EditTileButtonOnPressed;
			
			LeftPanel.AddChild(DeleteTileButton = new Button { Text = "Delete Tile" });
			DeleteTileButton.Pressed += DeleteTileButtonOnPressed;
			
			LeftPanel.AddChild(TileSelector = new TileSelectorPanel(levelInfo.TileSet));
		}
		SplitContainer.AddChild(AreaEditor = new AreaEditor(levelInfo));
	}
}