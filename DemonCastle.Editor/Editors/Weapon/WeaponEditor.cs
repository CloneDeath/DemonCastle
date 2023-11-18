using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon;

public partial class WeaponEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.WeaponIcon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }

	public WeaponEditor(WeaponInfo weapon) {
		Name = nameof(WeaponEditor);
		TabText = weapon.FileName;
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(new WeaponDetails(weapon));
	}
}