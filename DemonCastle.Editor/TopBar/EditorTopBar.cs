using Godot;

namespace DemonCastle.Editor.TopBar {
	public class EditorTopBar : HBoxContainer {
		public EditorTopBar() {
			AddChild(new Button {
				Text = "Play",
				Disabled = true
			});
		}
	}
}