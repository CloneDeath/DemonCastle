using DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;
using DemonCastle.Editor.Editors.Components.AnimationFrames;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor;

public partial class CharacterAnimationEditor : VSplitContainer {
	private VBoxContainer Top { get; }
	private CharacterAnimationDetails Details { get; }
	private FrameListEditor FrameList { get; }
	private CharacterFrameInfoDetails FrameDetails { get; }

	public CharacterAnimationEditor(CharacterInfo character) {
		Name = nameof(CharacterAnimationEditor);

		AddChild(Top = new VBoxContainer());
		Top.AddChild(Details = new CharacterAnimationDetails());
		Top.AddChild(FrameList = new FrameListEditor());
		FrameList.FrameSelected += FrameList_OnFrameSelected;

		AddChild(FrameDetails = new CharacterFrameInfoDetails(character));
	}


	private void FrameList_OnFrameSelected(IFrameInfo? frame) {
		FrameDetails.FrameInfo = frame;
	}

	public void LoadAnimation(IAnimationInfo animation) {
		Details.CharacterAnimation = animation;
		FrameList.Load(animation);
	}
}