using DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationEditor : VBoxContainer {
	private WeaponAnimationDetails Details { get; }
	private WeaponFrameListEditor FrameList { get; }

	public WeaponAnimationEditor() {
		Name = nameof(WeaponAnimationEditor);

		AddChild(Details = new WeaponAnimationDetails());
		AddChild(FrameList = new WeaponFrameListEditor());
	}

	public void LoadAnimation(WeaponAnimationInfo animation) {
		Details.WeaponAnimation = animation;
		FrameList.Load(animation);
	}
}