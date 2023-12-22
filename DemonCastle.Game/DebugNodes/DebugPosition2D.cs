using Godot;

namespace DemonCastle.Game.DebugNodes;

public partial class DebugPosition2D : Node2D {
	public DebugPosition2D(DebugState state) {
		Name = nameof(DebugPosition2D);
		Visible = state.ShowPositions;

		AddChild(new ColorRect {
			Color = Colors.White,
			Position = Vector2.Left,
			Size = new Vector2(3, 1)
		});
		AddChild(new ColorRect {
			Color = Colors.White,
			Position = Vector2.Up,
			Size = new Vector2(1, 3)
		});
	}
}