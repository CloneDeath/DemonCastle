using DemonCastle.Editor.Editors.Components.Animations;
using DemonCastle.Editor.Editors.Weapon.Details;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon;

public partial class WeaponEditor : BaseEditor {
	protected HSplitContainer SplitContainer { get; }

	public WeaponEditor(WeaponInfo weapon) {
		Name = nameof(WeaponEditor);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(new WeaponDetails(weapon) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		SplitContainer.AddChild(new AnimationsEditor(weapon) {
			CustomMinimumSize = new Vector2(300, 300),
			Animations = weapon.Animations
		});
	}
}