using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationEditor : VBoxContainer {
	private WeaponAnimationDetails Details { get; }

	public WeaponAnimationEditor() {
		Name = nameof(WeaponAnimationEditor);

		AddChild(Details = new WeaponAnimationDetails());
	}

	public void LoadAnimation(WeaponAnimationInfo animation) {
		Details.WeaponAnimation = animation;
	}
}