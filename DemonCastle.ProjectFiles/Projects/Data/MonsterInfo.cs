using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class MonsterInfo : FileInfo<MonsterFile>, IListableInfo, INotifyPropertyChanged {
	public MonsterInfo(FileNavigator<MonsterFile> file) : base(file) {
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float MoveSpeed {
		get => Resource.MoveSpeed;
		set {
			Resource.MoveSpeed = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float JumpHeight {
		get => Resource.JumpHeight;
		set {
			Resource.JumpHeight = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float Gravity {
		get => Resource.Gravity;
		set {
			Resource.Gravity = value;
			Save();
			OnPropertyChanged();
		}
	}

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