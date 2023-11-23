using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor;

public partial class AnimationFrameContainer : HFlowContainer {
	public void ClearChildren() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}
}