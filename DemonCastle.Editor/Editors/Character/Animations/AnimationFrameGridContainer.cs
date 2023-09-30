using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations; 

public partial class AnimationFrameGridContainer : GridContainer {
	public void ClearChildren() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}
}