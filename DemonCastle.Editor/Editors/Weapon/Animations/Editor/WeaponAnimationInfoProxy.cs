using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor;

public class WeaponAnimationInfoProxy : INotifyPropertyChanged {
	private WeaponAnimationInfo? _proxy;

	public WeaponAnimationInfo? Proxy {
		get => _proxy;
		set {
			if (_proxy != null) {
				_proxy.PropertyChanged -= Proxy_OnPropertyChanged;
			}

			_proxy = value;
			if (_proxy != null) {
				_proxy.PropertyChanged += Proxy_OnPropertyChanged;
			}

			OnPropertyChanged(nameof(Id));
			OnPropertyChanged(nameof(Name));
		}
	}

	public Guid Id => _proxy?.Id ?? Guid.NewGuid();

	public string Name {
		get => _proxy?.Name ?? string.Empty;
		set {
			if (_proxy != null) _proxy.Name = value;
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

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}