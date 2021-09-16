using Godot;

namespace DemonCastle.Editor.Windows.Character {
	public class SingleAnimationEditArea : VBoxContainer {
		protected GridContainer GridContainer { get; }
		public SingleAnimationEditArea() {
			AddChild(new LineEdit());
			AddChild(GridContainer = new GridContainer {
				Columns = 3
			});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
		}
	}
}