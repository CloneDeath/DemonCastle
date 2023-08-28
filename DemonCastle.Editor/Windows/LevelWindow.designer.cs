using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows; 

public partial class LevelWindow {
	protected PropertyCollection Properties { get; }

	public LevelWindow(LevelInfo levelInfo) {
		Title = $"Level - {levelInfo.FileName}";
		Size = new Vector2I(300, 300);
		MinSize = Size;
		
		AddChild(Properties = new PropertyCollection {
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = 205,
			OffsetBottom = -5,
			AnchorBottom = 1
		});
		Properties.AddString("Name", levelInfo, x => x.Name);
		Properties.AddInteger("Tile Width", levelInfo, x => x.TileWidth);
		Properties.AddInteger("Tile Height", levelInfo, x => x.TileHeight);
		Properties.AddInteger("Area Width", levelInfo, x => x.AreaWidth);
		Properties.AddInteger("Area Height", levelInfo, x => x.AreaHeight);
	}
}