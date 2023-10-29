using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Details;

public class AreaProxy : INotifyPropertyChanged {
	private AreaInfo? _proxy;

	public AreaInfo? Proxy {
		get => _proxy;
		set {
			if (_proxy != null) {
				_proxy.PropertyChanged -= Proxy_OnPropertyChanged;
			}

			_proxy = value;
			if (_proxy != null) {
				_proxy.PropertyChanged += Proxy_OnPropertyChanged;
			}

			OnPropertyChanged(nameof(Name));
			OnPropertyChanged(nameof(AreaPosition));
			OnPropertyChanged(nameof(Size));
		}
	}

	public string Name {
		get => _proxy?.Name ?? string.Empty;
		set {
			if (_proxy != null) _proxy.Name = value;
		}
	}

	public Vector2I AreaPosition {
		get => _proxy?.AreaPosition ?? Vector2I.Zero;
		set {
			if (_proxy != null) _proxy.AreaPosition = value;
		}
	}

	public Vector2I Size {
		get => _proxy?.Size ?? Vector2I.One;
		set {
			if (_proxy != null) _proxy.Size = value;
		}
	}

	private void Proxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		PropertyChanged?.Invoke(this, e);
	}

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	#endregion
}