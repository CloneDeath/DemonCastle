using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public class StateInfo : INotifyPropertyChanged {
	protected IFileNavigator File { get; }
	protected EntityStateData State { get; }

	public StateInfo(IFileNavigator file, EntityStateData state) {
		File = file;
		State = state;
		OnEnter = new EntityActionInfoCollection(file, state.OnEnter);
		OnUpdate = new EntityActionInfoCollection(file, state.OnUpdate);
		OnExit = new EntityActionInfoCollection(file, state.OnExit);
		Transitions = new EntityStateTransitionInfoCollection(file, state.Transitions);
	}

	public Guid Id => State.Id;

	public string Name {
		get => State.Name;
		set {
			State.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid Animation {
		get => State.Animation;
		set {
			State.Animation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public EntityActionInfoCollection OnEnter { get; }
	public EntityActionInfoCollection OnUpdate { get; }
	public EntityActionInfoCollection OnExit { get; }
	public EntityStateTransitionInfoCollection Transitions { get; }

	protected void Save() => File.Save();

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}