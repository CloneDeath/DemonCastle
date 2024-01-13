using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;

public class SpriteAtlasDataInfoProxy : ISpriteDefinition {
	private SpriteAtlasDataInfo? _proxy;

	public SpriteAtlasDataInfo? Proxy {
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
			OnPropertyChanged(nameof(Texture));
			OnPropertyChanged(nameof(Region));
			OnPropertyChanged(nameof(FlipHorizontal));
			OnPropertyChanged(nameof(TransparentColor));
			OnPropertyChanged(nameof(TransparentColorThreshold));
		}
	}

	public Guid Id => _proxy?.Id ?? Guid.NewGuid();

	public string Name {
		get => _proxy?.Name ?? string.Empty;
		set {
			if (_proxy != null) _proxy.Name = value;
		}
	}

	public Texture2D Texture => _proxy?.Texture ?? new NullSpriteDefinition().Texture;

	public Rect2I Region {
		get => _proxy?.Region ?? new Rect2I(Vector2I.Zero, Vector2I.One * 16);
		set {
			if (_proxy != null) _proxy.Region = value;
		}
	}

	public bool FlipHorizontal {
		get => _proxy?.FlipHorizontal ?? false;
		set {
			if (_proxy != null) _proxy.FlipHorizontal = value;
		}
	}

	public Color TransparentColor => _proxy?.TransparentColor ?? Colors.Transparent;
	public float TransparentColorThreshold => _proxy?.TransparentColorThreshold ?? 0.01f;

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