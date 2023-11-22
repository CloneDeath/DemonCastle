using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationDetails : PropertyCollection {
	private readonly WeaponAnimationInfoProxy _proxy = new();

	private StringProperty WeaponName { get; }

	public WeaponAnimationInfo? WeaponAnimation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponAnimationDetails() {
		Name = nameof(WeaponAnimationDetails);

		WeaponName = AddString("Name", _proxy, w => w.Name);
	}

	public void Disable() => WeaponName.Disable();
	public void Enable() => WeaponName.Enable();

	public override void _Process(double delta) {
		base._Process(delta);
		if (WeaponAnimation == null) Disable();
		else Enable();
	}
}