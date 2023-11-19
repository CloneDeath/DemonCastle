using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationDetails : PropertyCollection {
	private readonly WeaponAnimationInfoProxy _proxy = new();

	public WeaponAnimationInfo? WeaponAnimation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponAnimationDetails() {
		Name = nameof(WeaponAnimationDetails);

		AddString("Name", _proxy, w => w.Name);
	}
}