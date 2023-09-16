using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations; 

public partial class AnimationFrameGridContainer : GridContainer {
	public void ClearChildren() {
		foreach (Node child in GetChildren()) {
			child.QueueFree();
		}
	}
}