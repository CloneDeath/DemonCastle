using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition; 

public class SpriteGridDataInfo : ISpriteDefinition {
	protected SpriteGridData Data { get; }
	protected SpriteGridInfo SpriteGrid { get; }

	public SpriteGridDataInfo(SpriteGridInfo spriteGrid, SpriteGridData data) {
		SpriteGrid = spriteGrid;
		Data = data;
	}

	public string Name {
		get => Data.Name;
		set { Data.Name = value; Save(); }
	}

	public int X {
		get => Data.X;
		set { Data.X = value; Save(); }
	}

	public int Y {
		get => Data.Y;
		set { Data.Y = value; Save(); }
	}
	public Texture2D Texture => SpriteGrid.Texture;
	public Rect2I Region => new() {
		Position = Offset + Span * new Vector2I(Data.X, Data.Y),
		Size = Size
	};

	public bool FlipHorizontal {
		get => Data.FlipHorizontal;
		set { Data.FlipHorizontal = value; Save(); }
	}
	public Color TransparentColor => Colors.Transparent;
	public float TransparentColorThreshold => 0.001f;

	protected Vector2I Offset => SpriteGrid.Offset;
	protected Vector2I Span => SpriteGrid.Span;
	protected Vector2I Size => SpriteGrid.Size;

	public void Save() => SpriteGrid.Save();
	public void Remove() => SpriteGrid.Remove(Data, this);
}