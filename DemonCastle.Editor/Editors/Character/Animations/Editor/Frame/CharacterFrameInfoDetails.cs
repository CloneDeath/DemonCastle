using DemonCastle.Editor.Editors.Animations.Editor.Frames;
using DemonCastle.Editor.Editors.Properties.Vector;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameInfoDetails : FrameInfoDetails {

	public CharacterFrameInfoDetails(IFileInfo file) : base(file) {
		Name = nameof(CharacterFrameInfoDetails);

		AdditionalProperties.AddBoolean("Weapon Enabled", _proxy, p => p.WeaponEnabled);
		AdditionalProperties.AddString("Weapon Animation", _proxy, p => p.WeaponAnimation);
		AdditionalProperties.AddVector2I("Weapon Position", _proxy, p => p.WeaponPosition, new Vector2IPropertyOptions { AllowNegative = true });
	}
}