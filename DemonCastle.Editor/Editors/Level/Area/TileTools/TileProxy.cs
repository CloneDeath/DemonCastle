using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public class TileProxy : INotifyPropertyChanged {
	private TileInfo? _proxy;

	public TileInfo? Proxy {
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
			OnPropertyChanged(nameof(SourceFile));
			OnPropertyChanged(nameof(SpriteId));
			OnPropertyChanged(nameof(Directory));
			OnPropertyChanged(nameof(SpriteOptions));
		}
	}

	public string Name {
		get => _proxy?.Name ?? string.Empty;
		set {
			if (_proxy != null) _proxy.Name = value;
		}
	}

	public string SourceFile {
		get => _proxy?.SourceFile ?? string.Empty;
		set {
			if (_proxy != null) _proxy.SourceFile = value;
		}
	}

	public Guid SpriteId {
		get => _proxy?.SpriteId ?? Guid.Empty;
		set {
			if (_proxy != null) _proxy.SpriteId = value;
		}
	}

	public string Directory => _proxy?.Directory ?? string.Empty;
	public IEnumerable<ISpriteDefinition> SpriteOptions => _proxy?.SpriteOptions ?? Array.Empty<ISpriteDefinition>();

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