using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaWindow {
	protected HSplitContainer SplitContainer { get; }
	protected PropertyCollection Properties { get; }
	protected VSplitContainer ToolSplitContainer { get; }
	
	public AreaWindow(AreaInfo area) {
		Name = nameof(AreaWindow);
		Title = $"Area - X:{area.AreaPosition.X}, Y:{area.AreaPosition.Y}";
		Size = new Vector2I(400, 100) + area.Size * area.AreaSize * area.TileSize;
		MinSize = Size;
		
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

		ToolSplitContainer.AddChild(new TileSelectorPanel(area.TileSet));
		
		SplitContainer.AddChild(new AreaTileEditor(area));
	}
}