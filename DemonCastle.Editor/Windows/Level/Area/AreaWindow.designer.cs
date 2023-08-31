using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaWindow {
	public AreaWindow(AreaInfo areaInfo) {
		Title = $"Area - X:{areaInfo.AreaPosition.X}, Y:{areaInfo.AreaPosition.Y}";
	}
}