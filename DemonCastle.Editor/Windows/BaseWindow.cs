using Godot;

namespace DemonCastle.Editor.Windows; 

public partial class BaseWindow : Window {
	public BaseWindow() {
		Unresizable = false;
		CloseRequested += Hide;
	}
		
	public override void _Process(double delta) {
		base._Process(delta);

		if (Position.X < GetParent<Control>().Position.X) {
			Position = new Vector2I((int)GetParent<Control>().Position.X, Position.Y);
		}

		if (Position.Y < GetParent<Control>().Position.Y + 20) {
			Position = new Vector2I(Position.X, (int)GetParent<Control>().Position.Y + 20);
		}
	}
}