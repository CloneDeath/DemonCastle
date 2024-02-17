using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteDefinition : ISpriteDefinition {
	public Guid Id => Guid.Empty;
	public string Name => "null";
	public Texture2D Texture => new GradientTexture2D {
		Gradient = new Gradient {
			Colors = new []{Colors.Red, Colors.Blue},
			Offsets = new []{0f, 1f}
		},
		Width = 16
	};
	public Rect2I Region => new(0, 0, 16, 16);
	public bool FlipHorizontal => false;
	public bool FlipVertical => false;
	public Color TransparentColor => Colors.Transparent;
	public float TransparentColorThreshold => 0.01f;

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