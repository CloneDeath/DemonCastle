using System;
using DemonCastle.ProjectFiles.Projects.Data.States;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public class StateInfoProxy : InfoProxy<StateInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Id));
		OnPropertyChanged(nameof(Name));
		OnPropertyChanged(nameof(Animation));
	}

	public Guid Id => Proxy?.Id ?? Guid.NewGuid();

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set {
			if (Proxy != null) Proxy.Name = value;
		}
	}

	public Guid Animation {
		get => Proxy?.Animation ?? Guid.Empty;
		set {
			if (Proxy != null) Proxy.Animation = value;
		}
	}
}