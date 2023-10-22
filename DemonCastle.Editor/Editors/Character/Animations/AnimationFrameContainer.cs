using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class AnimationFrameContainer : HFlowContainer {
	public void ClearChildren() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}
}