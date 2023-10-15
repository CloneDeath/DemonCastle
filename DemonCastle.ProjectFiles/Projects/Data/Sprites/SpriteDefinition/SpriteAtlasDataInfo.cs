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

	public Vector2I Position {
		get => new(Data.X, Data.Y);
		set {
			X = value.X;
			Y = value.Y;
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => new(Data.Width, Data.Height);
		set {
			Width = value.X;
			Height = value.Y;
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

	public int Width {
		get => Data.Width;
		set {
			Data.Width = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Height {
		get => Data.Height;
		set {
			Data.Height = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Texture2D Texture => SpriteAtlasInfo.Texture;

	public Rect2I Region {
		get => new(Position, Size);
		set {
			Position = value.Position;
			Size = value.Size;
			OnPropertyChanged();
		}
	}

	public bool FlipHorizontal => Data.FlipHorizontal;
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