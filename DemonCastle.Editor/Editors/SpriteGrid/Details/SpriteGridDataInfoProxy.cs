using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public class SpriteGridDataInfoProxy : ISpriteDefinition {
	private SpriteGridDataInfo? _proxy;

	public SpriteGridDataInfo? Proxy {
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
			OnPropertyChanged(nameof(X));
			OnPropertyChanged(nameof(Y));
			OnPropertyChanged(nameof(Texture));
			OnPropertyChanged(nameof(Region));
			OnPropertyChanged(nameof(FlipHorizontal));
			OnPropertyChanged(nameof(FlipVertical));
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

	public int X {
		get => _proxy?.X ?? 0;
		set {
			if (_proxy != null) _proxy.X = value;
		}
	}

	public int Y {
		get => _proxy?.Y ?? 0;
		set {
			if (_proxy != null) _proxy.Y = value;
		}
	}

	public Texture2D Texture => _proxy?.Texture ?? new NullSpriteDefinition().Texture;

	public Rect2I Region => _proxy?.Region ?? new Rect2I(Vector2I.Zero, Vector2I.One * 16);

	public bool FlipHorizontal {
		get => _proxy?.FlipHorizontal ?? false;
		set {
			if (_proxy != null) _proxy.FlipHorizontal = value;
		}
	}

	public bool FlipVertical {
		get => _proxy?.FlipVertical ?? false;
		set {
			if (_proxy != null) _proxy.FlipVertical = value;
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