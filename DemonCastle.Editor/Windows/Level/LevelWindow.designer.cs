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
		Size = new Vector2I(600, 400);
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
		Properties.AddVector2I("Tile Size", levelInfo, x => x.TileSize);
		Properties.AddVector2I("Area Size", levelInfo, x => x.AreaSize);

		SplitContainer.AddChild(new AreaEditor(levelInfo));
	}
}