using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
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
		foreach (var frame in Current.Frames) {
			FrameContainer.AddChild(new Frame.AnimationFramePanel(frame));
		}
	}

	protected void OnAddFrameButtonPressed() {
		CurrentAnimation.AddFrame();
		BindAnimation();
	}
}