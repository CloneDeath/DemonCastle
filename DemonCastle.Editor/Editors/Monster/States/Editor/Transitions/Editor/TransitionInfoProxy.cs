using System;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor;

public class TransitionInfoProxy : InfoProxy<TransitionInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Id));
		OnPropertyChanged(nameof(Name));
		OnPropertyChanged(nameof(TargetState));
	}

	public Guid Id => Proxy?.Id ?? Guid.NewGuid();

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set {
			if (Proxy != null) Proxy.Name = value;
		}
	}

	public Guid TargetState {
		get => Proxy?.TargetState ?? Guid.Empty;
		set {
			if (Proxy != null) Proxy.TargetState = value;
		}
	}

	public WhenInfo? When => Proxy?.When;
}