using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public partial class WeaponAnimationDetails : PropertyCollection {
	private readonly WeaponAnimationInfoProxy _proxy = new();

	private StringProperty AnimationName { get; }

	public AnimationInfo? WeaponAnimation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponAnimationDetails() {
		Name = nameof(WeaponAnimationDetails);

		AnimationName = AddString("Name", _proxy, w => w.Name);
	}

	public void Disable() => AnimationName.Disable();
	public void Enable() => AnimationName.Enable();

	public override void _Process(double delta) {
		base._Process(delta);
		if (WeaponAnimation == null) Disable();
		else Enable();
	}
}