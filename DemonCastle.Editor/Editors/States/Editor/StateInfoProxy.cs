using System;
using DemonCastle.Editor.Editors.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.States;

namespace DemonCastle.Editor.Editors.States.Editor;

public class StateInfoProxy : InfoProxy<StateInfo> {
	protected override void NotifyProxyChanged() {
		Transitions.Proxy = Proxy;
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

	public TransitionInfoCollectionProxy Transitions { get; } = new();
}