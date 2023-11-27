using System;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public class WeaponAnimationInfoProxy : InfoProxy<AnimationInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Id));
		OnPropertyChanged(nameof(Name));
	}

	public Guid Id => Proxy?.Id ?? Guid.NewGuid();

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set {
			if (Proxy != null) Proxy.Name = value;
		}
	}
}