using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaCell : Node2D {
	public AreaCell(AreaInfo areaInfo) {
		var levelInfo = areaInfo.LevelInfo;
		Position = areaInfo.AreaPosition * levelInfo.AreaSize;
		AddChild(new ColorRect {
			Color = Colors.Blue,
			Size = areaInfo.AreaSize * levelInfo.AreaSize
		});
		AddChild(new ColorRect {
			Position = new Vector2(1, 1),
			Color = Colors.LightBlue,
			Size = areaInfo.AreaSize * levelInfo.AreaSize - new Vector2I(2, 2)
		});
	}
}