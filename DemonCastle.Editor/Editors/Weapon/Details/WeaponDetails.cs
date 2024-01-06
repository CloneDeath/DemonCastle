using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Weapon.Details;

public partial class WeaponDetails : PropertyCollection {
	public WeaponDetails(WeaponInfo weapon) {
		Name = nameof(WeaponDetails);

		AddString("Name", weapon, w => w.Name);
	}
}