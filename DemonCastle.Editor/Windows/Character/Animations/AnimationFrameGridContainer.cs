using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationFrameGridContainer : GridContainer {
		public void ClearChildren() {
			foreach (Node child in GetChildren()) {
				child.QueueFree();
			}
		}
	}
}