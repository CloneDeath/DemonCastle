using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class BaseWindow : Window {
		public BaseWindow() {
			Resizable = true;
		}
		
		public override void _Process(float delta) {
			base._Process(delta);

			if (GlobalPosition.x < GetParentControl().GlobalPosition.x) {
				GlobalPosition = new Vector2(GetParentControl().GlobalPosition.x, GlobalPosition.y);
			}

			if (GlobalPosition.y < GetParentControl().GlobalPosition.y + 20) {
				GlobalPosition = new Vector2(GlobalPosition.x, GetParentControl().GlobalPosition.y + 20);
			}
		}
	}
}