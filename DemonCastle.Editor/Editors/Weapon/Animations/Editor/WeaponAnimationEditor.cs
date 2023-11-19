using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationEditor : VBoxContainer {
	public WeaponAnimationEditor() {
		AddChild(new Label {Text = "Weapon Animation Editor"});
	}
}