using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DemonCastle.Editor;

public abstract class InfoProxy<T> : INotifyPropertyChanged where T : INotifyPropertyChanged {
	private T? _proxy;

	public T? Proxy {
		get => _proxy;
		set {
			if (_proxy != null) {
				_proxy.PropertyChanged -= Proxy_OnPropertyChanged;
			}
			_proxy = value;
			if (_proxy != null) {
				_proxy.PropertyChanged += Proxy_OnPropertyChanged;
			}

			NotifyProxyChanged();
		}
	}

	protected abstract void NotifyProxyChanged();

	private void Proxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		PropertyChanged?.Invoke(this, e);
	}

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<F>(ref F field, F value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<F>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}