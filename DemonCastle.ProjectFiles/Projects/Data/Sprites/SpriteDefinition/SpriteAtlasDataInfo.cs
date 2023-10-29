using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

public class SpriteAtlasDataInfo : ISpriteDefinition, INotifyPropertyChanged {
	public SpriteAtlasDataInfo(SpriteAtlasInfo spriteAtlasInfo, SpriteAtlasData data) {
		SpriteAtlasInfo = spriteAtlasInfo;
		Data = data;
	}

	protected SpriteAtlasInfo SpriteAtlasInfo { get; }
	protected SpriteAtlasData Data { get; }

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
			OnPropertyChanged(nameof(Position));
			OnPropertyChanged(nameof(Region));
		}
	}

	public int Y {
		get => Data.Y;
		set {
			Data.Y = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Position));
			OnPropertyChanged(nameof(Region));
		}
	}

	public Vector2I Position {
		get => new(Data.X, Data.Y);
		set {
			if (!SetField(ref Data.X, value.X, nameof(X)) && !SetField(ref Data.Y, value.Y, nameof(Y))) {
				return;
			}
			OnPropertyChanged();
			OnPropertyChanged(nameof(Region));
			Save();
		}
	}

	public int Width {
		get => Data.Width;
		set {
			Data.Width = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Size));
			OnPropertyChanged(nameof(Region));
		}
	}

	public int Height {
		get => Data.Height;
		set {
			Data.Height = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Size));
			OnPropertyChanged(nameof(Region));
		}
	}

	public Vector2I Size {
		get => new(Data.Width, Data.Height);
		set {
			Data.Width = value.X;
			Data.Height = value.Y;
			Save();
			OnPropertyChanged(nameof(Width));
			OnPropertyChanged(nameof(Height));
			OnPropertyChanged();
			OnPropertyChanged(nameof(Region));
		}
	}

	public Rect2I Region {
		get => new(Position, Size);
		set {
			Data.X = value.Position.X;
			Data.Y = value.Position.Y;
			Data.Width = value.Size.X;
			Data.Height = value.Size.Y;
			Save();
			OnPropertyChanged(nameof(X));
			OnPropertyChanged(nameof(Y));
			OnPropertyChanged(nameof(Position));
			OnPropertyChanged(nameof(Width));
			OnPropertyChanged(nameof(Height));
			OnPropertyChanged(nameof(Size));
			OnPropertyChanged();
		}
	}

	public bool FlipHorizontal {
		get => Data.FlipHorizontal;
		set {
			Data.FlipHorizontal = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Texture2D Texture => SpriteAtlasInfo.Texture;

	public Color TransparentColor => SpriteAtlasInfo.TransparentColor;
	public float TransparentColorThreshold => 0.001f;

	protected void Save() => SpriteAtlasInfo.Save();

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