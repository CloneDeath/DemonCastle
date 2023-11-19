using DemonCastle.Editor.Editors.Weapon.Animations.Editor;
using DemonCastle.Editor.Editors.Weapon.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations;

public partial class WeaponAnimations : HSplitContainer {
	public WeaponAnimations(WeaponInfo weapon) {
		Name = nameof(WeaponAnimations);

		AddChild(new WeaponAnimationList {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AddChild(new WeaponAnimationEditor {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}
}