using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon;

public partial class WeaponEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.WeaponIcon;
	public override string TabText { get; }

	public WeaponEditor(WeaponInfo characterInfo) {
		Name = nameof(WeaponEditor);
		TabText = characterInfo.FileName;
		CustomMinimumSize = new Vector2I(600, 300);
	}
}