using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class LevelWindow {
	protected HSplitContainer SplitContainer { get; }
	protected PropertyCollection Properties { get; }

	public LevelWindow(LevelInfo levelInfo) {
		Name = nameof(LevelWindow);
		Title = $"Level - {levelInfo.FileName}";
		Size = new Vector2I(300, 300);
		MinSize = Size;
		
		AddChild(SplitContainer = new HSplitContainer {
			AnchorBottom = 1,
			AnchorRight = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = -5,
			OffsetBottom = -5
		});
		
		SplitContainer.AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", levelInfo, x => x.Name);
		Properties.AddInteger("Tile Width", levelInfo, x => x.TileWidth);
		Properties.AddInteger("Tile Height", levelInfo, x => x.TileHeight);
		Properties.AddInteger("Area Width", levelInfo, x => x.AreaWidth);
		Properties.AddInteger("Area Height", levelInfo, x => x.AreaHeight);

		SplitContainer.AddChild(new AreaEditor(levelInfo));
	}
}