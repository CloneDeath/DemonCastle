using DemonCastle.Editor.Editors.Weapon.Animations.Editor;
using DemonCastle.Editor.Editors.Weapon.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations;

public partial class WeaponAnimations : HSplitContainer {
	private WeaponAnimationList AnimationList { get; }
	private WeaponAnimationEditor AnimationEditor { get; }

	public WeaponAnimations(WeaponInfo weapon) {
		Name = nameof(WeaponAnimations);

		AddChild(AnimationList = new WeaponAnimationList(weapon) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.AnimationSelected += AnimationList_OnAnimationSelected;

		AddChild(AnimationEditor = new WeaponAnimationEditor(weapon) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	private void AnimationList_OnAnimationSelected(AnimationInfo animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}