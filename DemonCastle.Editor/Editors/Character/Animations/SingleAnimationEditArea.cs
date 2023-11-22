using DemonCastle.Editor.Editors.Character.Animations.Frame;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class SingleAnimationEditArea : VBoxContainer {
	protected CharacterAnimationInfo Current;
	public CharacterAnimationInfo CurrentAnimation {
		get => Current;
		set {
			Current = value;
			BindAnimation();
		}
	}

	protected void BindAnimation() {
		LineEdit.Binding = new PropertyBinding<CharacterAnimationInfo, string>(Current, animation => animation.Name);

		FrameContainer.ClearChildren();
		foreach (var frame in Current.CharacterFrames) {
			FrameContainer.AddChild(new AnimationFramePanel(frame));
		}
	}

	protected void OnAddFrameButtonPressed() {
		CurrentAnimation.AddFrame();
		BindAnimation();
	}
}