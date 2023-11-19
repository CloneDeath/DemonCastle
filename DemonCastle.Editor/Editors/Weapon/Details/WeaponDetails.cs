using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Weapon.Details;

public partial class WeaponDetails : PropertyCollection {
	public WeaponDetails(WeaponInfo weapon) {
		Name = nameof(WeaponDetails);

		AddString("Name", weapon, w => w.Name);
	}
}