using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class GridControl : Control {
	public Vector2I GridSize { get; set; } = new(16, 16);

	public GridControl() {
		MouseFilter = MouseFilterEnum.Ignore;
	}
	
	public override void _Draw() {
		base._Draw();
		
		var gridColor = new Color(Colors.White, 0.4f);
		for (var x = 0; x <= Size.X; x += GridSize.X) {
			DrawLine(new Vector2(x, 0), new Vector2(x, Size.Y), gridColor);
		}
		
		for (var y = 0; y <= Size.Y; y += GridSize.Y) {
			DrawLine(new Vector2(0, y), new Vector2(Size.X, y), gridColor);
		}
	}
}