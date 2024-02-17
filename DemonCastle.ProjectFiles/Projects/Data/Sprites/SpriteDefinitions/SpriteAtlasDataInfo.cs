using System;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

public class SpriteAtlasDataInfo : BaseInfo<SpriteAtlasData>, ISpriteDefinition {
	public SpriteAtlasDataInfo(IFileNavigator file, SpriteAtlasData data, SpriteAtlasInfo spriteAtlas) : base(file, data) {
		SpriteAtlas = spriteAtlas;
	}

	protected SpriteAtlasInfo SpriteAtlas { get; }

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set => SaveField(ref Data.Name, value);
	}

	public int X {
		get => Data.X;
		set {
			if (!SaveField(ref Data.X, value)) return;
			OnPropertyChanged(nameof(Position));
			OnPropertyChanged(nameof(Region));
		}
	}

	public int Y {
		get => Data.Y;
		set {
			if (!SaveField(ref Data.Y, value)) return;
			OnPropertyChanged(nameof(Position));
			OnPropertyChanged(nameof(Region));
		}
	}

	public Vector2I Position {
		get => new(Data.X, Data.Y);
		set {
			if (!SaveField(ref Data.X, value.X, nameof(X)) && !SaveField(ref Data.Y, value.Y, nameof(Y))) {
				return;
			}
			OnPropertyChanged();
			OnPropertyChanged(nameof(Region));
		}
	}

	public int Width {
		get => Data.Width;
		set {
			if (!SaveField(ref Data.Width, value)) return;
			OnPropertyChanged(nameof(Size));
			OnPropertyChanged(nameof(Region));
		}
	}

	public int Height {
		get => Data.Height;
		set {
			if (!SaveField(ref Data.Height, value)) return;
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
		set => SaveField(ref Data.FlipHorizontal, value);
	}

	public bool FlipVertical {
		get => Data.FlipVertical;
		set => SaveField(ref Data.FlipVertical, value);
	}

	public Texture2D Texture => SpriteAtlas.Texture;

	public Color TransparentColor => SpriteAtlas.TransparentColor;
	public float TransparentColorThreshold => 0.001f;
}