using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaWindow {
	protected HSplitContainer SplitContainer { get; }
	protected PropertyCollection Properties { get; }
	
	public AreaWindow(AreaInfo areaInfo) {
		Name = nameof(AreaWindow);
		Title = $"Area - X:{areaInfo.AreaPosition.X}, Y:{areaInfo.AreaPosition.Y}";
		Size = new Vector2I(400, 400);
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
		Properties.AddString("Name", areaInfo, x => x.Name);
		Properties.AddVector2I("Position", areaInfo, x => x.AreaPosition);
		Properties.AddVector2I("Size", areaInfo, x => x.AreaSize);
	}
}