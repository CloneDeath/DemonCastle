using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.Files;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

public class SpriteGridDataInfo : ISpriteDefinition {
	public SpriteGridDataInfo(SpriteGridInfo spriteGrid, SpriteGridData data) {
		SpriteGrid = spriteGrid;
		Data = data;
		spriteGrid.PropertyChanged += SpriteGrid_OnPropertyChanged;
	}

	private void SpriteGrid_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName is nameof(SpriteGridInfo.Width)
			or nameof(SpriteGridInfo.Height)
			or nameof(SpriteGridInfo.XOffset)
			or nameof(SpriteGridInfo.YOffset)
			or nameof(SpriteGridInfo.XSeparation)
			or nameof(SpriteGridInfo.YSeparation)) {
			OnPropertyChanged(nameof(Region));
		}
	}

	protected SpriteGridData Data { get; }
	protected SpriteGridInfo SpriteGrid { get; }

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int X {
		get => Data.X;
		set {
			Data.X = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Y {
		get => Data.Y;
		set {
			Data.Y = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected Vector2I Offset => SpriteGrid.Offset;
	protected Vector2I Span => SpriteGrid.Span;
	protected Vector2I Size => SpriteGrid.Size;

	public Texture2D Texture => SpriteGrid.Texture;

	public Rect2I Region =>
		new() {
			Position = Offset + Span * new Vector2I(Data.X, Data.Y),
			Size = Size
		};

	public bool FlipHorizontal {
		get => Data.FlipHorizontal;
		set {
			Data.FlipHorizontal = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Color TransparentColor => Colors.Transparent;
	public float TransparentColorThreshold => 0.001f;

	public void Save() => SpriteGrid.Save();
	public void Remove() => SpriteGrid.GridSprites.Remove(this);

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