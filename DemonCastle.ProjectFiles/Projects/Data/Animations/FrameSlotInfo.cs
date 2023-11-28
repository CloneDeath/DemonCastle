using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameSlotInfo : INotifyPropertyChanged {
	string Name { get; set; }
	string Animation { get; set; }
	Vector2I Position { get; set; }
}

public class FrameSlotInfo : IFrameSlotInfo {
	private readonly IFileNavigator _file;
	private readonly FrameSlotData _data;

	public FrameSlotInfo(IFileNavigator file, FrameSlotData data) {
		_file = file;
		_data = data;
	}

	public string Name {
		get => _data.Name;
		set {
			_data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string Animation {
		get => _data.Animation;
		set {
			_data.Animation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Position {
		get => _data.Position;
		set {
			_data.Position = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected void Save() => _file.Save();

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