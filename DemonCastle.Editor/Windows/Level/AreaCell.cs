using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaCell : Node2D {
	public AreaCell(AreaInfo areaInfo) {
		var levelInfo = areaInfo.LevelInfo;
		Position = areaInfo.AreaPosition * levelInfo.AreaSize;

		const int borderWidth = 2;
		AddChild(new ColorRect {
			Color = Colors.LightBlue,
			Size = areaInfo.AreaSize * levelInfo.AreaSize
		});
		AddChild(new ColorRect {
			Position = new Vector2(1, 1) * borderWidth,
			Color = Colors.Blue,
			Size = areaInfo.AreaSize * levelInfo.AreaSize - new Vector2I(2, 2) * borderWidth
		});
	}
}