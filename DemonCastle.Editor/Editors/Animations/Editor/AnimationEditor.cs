using DemonCastle.Editor.Editors.Animations.Editor.Frames;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;
using FrameListEditor = DemonCastle.Editor.Editors.Components.AnimationFrames.FrameListEditor;

namespace DemonCastle.Editor.Editors.Animations.Editor;

public partial class AnimationEditor : VSplitContainer {
	private VBoxContainer Top { get; }
	private AnimationDetails Details { get; }
	private FrameListEditor FrameList { get; }
	private FrameInfoDetails FrameDetails { get; }

	public AnimationEditor(IEnumerableInfo<IAnimationInfo> animation) {
		Name = nameof(AnimationEditor);

		AddChild(Top = new VBoxContainer());
		Top.AddChild(Details = new AnimationDetails());
		Top.AddChild(FrameList = new FrameListEditor());
		FrameList.FrameSelected += FrameList_OnFrameSelected;

		AddChild(FrameDetails = new FrameInfoDetails(animation));
	}

	private void FrameList_OnFrameSelected(IFrameInfo frame) {
		FrameDetails.FrameInfo = frame as FrameInfo;
	}

	public void LoadAnimation(IAnimationInfo animation) {
		Details.Animation = animation;
		FrameList.Load(animation);
	}
}