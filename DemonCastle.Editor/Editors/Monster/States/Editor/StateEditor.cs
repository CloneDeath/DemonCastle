using DemonCastle.Editor.Editors.Animations.Editor;
using DemonCastle.Editor.Editors.Animations.Editor.Frames;
using DemonCastle.Editor.Editors.Components.AnimationFrames;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public partial class StateEditor : VSplitContainer {
	// private VBoxContainer Top { get; }
	// private AnimationDetails Details { get; }
	// private FrameListEditor FrameList { get; }
	// private FrameInfoDetails FrameDetails { get; }

	public StateEditor(IFileInfo file) {
		Name = nameof(StateEditor);

		// AddChild(Top = new VBoxContainer());
		// Top.AddChild(Details = new AnimationDetails());
		// Top.AddChild(FrameList = new FrameListEditor());
		// FrameList.FrameSelected += FrameList_OnFrameSelected;
		//
		// AddChild(FrameDetails = new FrameInfoDetails(file));
	}

	// private void FrameList_OnFrameSelected(IFrameInfo? frame) {
	// 	FrameDetails.FrameInfo = frame;
	// }

	public void LoadState(StateInfo state) {
		// Details.Animation = animation;
		// FrameList.Load(animation);
	}
}