using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.List;

public partial class WeaponAnimationList : VBoxContainer {
	public WeaponAnimationList() {
		Name = nameof(WeaponAnimationList);

		AddChild(new Button { Text = "Add" });
	}
}