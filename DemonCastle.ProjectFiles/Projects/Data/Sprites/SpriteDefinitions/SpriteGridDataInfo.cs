using System;
using System.ComponentModel;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

public class SpriteGridDataInfo : BaseInfo<SpriteGridData>, ISpriteDefinition {
	protected SpriteGridInfo SpriteGrid { get; }

	public SpriteGridDataInfo(IFileNavigator file, SpriteGridData data, SpriteGridInfo spriteGrid) : base(file, data) {
		SpriteGrid = spriteGrid;
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

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set => SaveField(ref Data.Name, value);
	}

	public int X {
		get => Data.X;
		set => SaveField(ref Data.X, value);
	}

	public int Y {
		get => Data.Y;
		set => SaveField(ref Data.Y, value);
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
		set => SaveField(ref Data.FlipHorizontal, value);
	}

	public bool FlipVertical {
		get => Data.FlipVertical;
		set => SaveField(ref Data.FlipVertical, value);
	}

	public Color TransparentColor => Colors.Transparent;
	public float TransparentColorThreshold => 0.001f;

	public void Remove() => SpriteGrid.GridSprites.Remove(this);
}