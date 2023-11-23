using DemonCastle.Editor.Controls;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;
using DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor;

public partial class SingleAnimationEditArea : VBoxContainer {
	protected CharacterAnimationInfo? Current;
	protected BindingLineEdit LineEdit { get; }
	protected AnimationFrameContainer FrameContainer { get; }

	public CharacterAnimationInfo? CurrentAnimation {
		get => Current;
		set {
			Current = value;
			BindAnimation();
		}
	}

	public SingleAnimationEditArea() {
		AddChild(LineEdit = new BindingLineEdit());
		AddChild(FrameContainer = new AnimationFrameContainer());
	}

	protected void BindAnimation() {
		if (Current == null) return;
		LineEdit.Binding = new PropertyBinding<CharacterAnimationInfo, string>(Current, animation => animation.Name);

		FrameContainer.ClearChildren();
		foreach (var frame in Current.CharacterFrames) {
			FrameContainer.AddChild(new AnimationFramePanel(frame));
		}
	}
}