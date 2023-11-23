using DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;
using FrameListEditor = DemonCastle.Editor.Editors.Components.AnimationFrames.FrameListEditor;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationEditor : VSplitContainer {
	private VBoxContainer Top { get; }
	private WeaponAnimationDetails Details { get; }
	private FrameListEditor FrameList { get; }
	private WeaponFrameInfoDetails FrameDetails { get; }

	public WeaponAnimationEditor(WeaponInfo weapon) {
		Name = nameof(WeaponAnimationEditor);

		AddChild(Top = new VBoxContainer());
		Top.AddChild(Details = new WeaponAnimationDetails());
		Top.AddChild(FrameList = new FrameListEditor());
		FrameList.FrameSelected += FrameList_OnFrameSelected;

		AddChild(FrameDetails = new WeaponFrameInfoDetails(weapon));
	}

	private void FrameList_OnFrameSelected(IFrameInfo frame) {
		FrameDetails.WeaponFrameInfo = frame as WeaponFrameInfo;
	}

	public void LoadAnimation(WeaponAnimationInfo animation) {
		Details.WeaponAnimation = animation;
		FrameList.Load(animation);
	}
}