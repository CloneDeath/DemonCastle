using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaCell : Node2D {
	public AreaCell(AreaInfo areaInfo) {
		var levelInfo = areaInfo.LevelInfo;
		Position = areaInfo.AreaPosition * levelInfo.AreaSize;
		AddChild(new ColorRect {
			Color = Colors.Blue,
			Size = areaInfo.AreaSize
		});
	}
}