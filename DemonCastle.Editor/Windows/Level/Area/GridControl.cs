using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class GridControl : Control {
	public Vector2I TotalSize { get; set; } = new(16, 16);
	public Vector2I GridSize { get; set; } = new(16, 16);

	public GridControl() {
		MouseFilter = MouseFilterEnum.Ignore;
	}
	
	public override void _Draw() {
		base._Draw();
		
		for (var x = 0; x <= TotalSize.X; x += GridSize.X) {
			DrawLine(new Vector2(x, 0), new Vector2(x, TotalSize.Y), Colors.White);
		}
		
		for (var y = 0; y <= TotalSize.Y; y += GridSize.Y) {
			DrawLine(new Vector2(0, y), new Vector2(TotalSize.X, y), Colors.White);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Size = TotalSize;
		QueueRedraw();
	}
}