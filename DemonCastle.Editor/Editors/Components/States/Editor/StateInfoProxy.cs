using System;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

namespace DemonCastle.Editor.Editors.Components.States.Editor;

public class StateInfoProxy : InfoProxy<EntityStateInfo> {
	protected override void NotifyProxyChanged() {
		Transitions.Proxy = Proxy?.Transitions;
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

	public EnumerableInfoProxy<EntityStateTransitionInfo> Transitions { get; } = new();
}