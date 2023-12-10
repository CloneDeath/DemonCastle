using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public class StateInfo : INotifyPropertyChanged {
	protected IFileNavigator File { get; }
	protected MonsterStateData State { get; }

	public StateInfo(IFileNavigator file, MonsterStateData state) {
		File = file;
		State = state;
		OnEnter = new ActionInfoCollection(file, state.OnEnter);
		OnUpdate = new ActionInfoCollection(file, state.OnUpdate);
		OnExit = new ActionInfoCollection(file, state.OnExit);
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

	public ActionInfoCollection OnEnter { get; }
	public ActionInfoCollection OnUpdate { get; }
	public ActionInfoCollection OnExit { get; }

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