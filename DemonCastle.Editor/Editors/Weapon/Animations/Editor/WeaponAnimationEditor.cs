using DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationEditor : VSplitContainer {
	private VBoxContainer Top { get; }
	private WeaponAnimationDetails Details { get; }
	private WeaponFrameListEditor FrameList { get; }
	private WeaponFrameInfoEdit FrameEdit { get; }

	public WeaponAnimationEditor(WeaponInfo weapon) {
		Name = nameof(WeaponAnimationEditor);

		AddChild(Top = new VBoxContainer());
		Top.AddChild(Details = new WeaponAnimationDetails());
		Top.AddChild(FrameList = new WeaponFrameListEditor());
		FrameList.FrameSelected += FrameList_OnFrameSelected;

		AddChild(FrameEdit = new WeaponFrameInfoEdit(weapon));
	}

	private void FrameList_OnFrameSelected(WeaponFrameInfo frame) {
		FrameEdit.WeaponFrameInfo = frame;
	}

	public void LoadAnimation(WeaponAnimationInfo animation) {
		Details.WeaponAnimation = animation;
		FrameList.Load(animation);
	}
}