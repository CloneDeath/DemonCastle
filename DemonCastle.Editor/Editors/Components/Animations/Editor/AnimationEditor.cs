using System.Linq;
using DemonCastle.Editor.Editors.Components.AnimationFrames;
using DemonCastle.Editor.Editors.Components.Animations.Editor.Frames;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations.Editor;

public partial class AnimationEditor : VSplitContainer {
	private VBoxContainer Top { get; }
	private AnimationDetails Details { get; }
	private FrameListEditor FrameList { get; }
	private FrameInfoDetails FrameDetails { get; }

	public AnimationEditor(IFileInfo file) {
		Name = nameof(AnimationEditor);

		AddChild(Top = new VBoxContainer());
		Top.AddChild(Details = new AnimationDetails());
		Top.AddChild(FrameList = new FrameListEditor());
		FrameList.FrameSelected += FrameList_OnFrameSelected;

		AddChild(FrameDetails = new FrameInfoDetails(file));
	}

	private void FrameList_OnFrameSelected(IFrameInfo? frame) {
		FrameDetails.FrameInfo = frame;
	}

	public void LoadAnimation(IAnimationInfo? animation) {
		Details.Animation = animation;
		FrameList.Load(animation);
		FrameDetails.FrameInfo = animation?.Frames.FirstOrDefault();
	}
}