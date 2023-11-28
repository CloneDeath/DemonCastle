using System;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Animations.Editor;

public class AnimationInfoProxy : InfoProxy<IAnimationInfo> {
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