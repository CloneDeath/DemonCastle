using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition; 

public class SpriteAtlasDataInfo : ISpriteDefinition {
	protected SpriteAtlasInfo SpriteAtlasInfo { get; }
	protected SpriteAtlasData Data { get; }

	public SpriteAtlasDataInfo(SpriteAtlasInfo spriteAtlasInfo, SpriteAtlasData data) {
		SpriteAtlasInfo = spriteAtlasInfo;
		Data = data;
	}

	public string Name {
		get => Data.Name;
		set { Data.Name = value; Save(); }
	}

	public Texture2D Texture => SpriteAtlasInfo.Texture;

	public Rect2I Region {
		get => new(Position, Size);
		set {
			Position = value.Position;
			Size = value.Size;
		}
	}

	public Vector2I Position {
		get => new(Data.X, Data.Y);
		set {
			X = value.X;
			Y = value.Y;
		}
	}

	public Vector2I Size {
		get => new(Data.Width, Data.Height);
		set {
			Width = value.X;
			Height = value.Y;
		}
	}

	public bool FlipHorizontal => Data.FlipHorizontal;
	public Color TransparentColor => SpriteAtlasInfo.TransparentColor;
	public float TransparentColorThreshold => 0.001f;

	public int X {
		get => Data.X;
		set { Data.X = value; Save(); }
	}

	public int Y {
		get => Data.Y;
		set { Data.Y = value; Save(); }
	}

	public int Width {
		get => Data.Width;
		set { Data.Width = value; Save(); }
	}

	public int Height {
		get => Data.Height;
		set { Data.Height = value; Save(); }
	}

	protected void Save() => SpriteAtlasInfo.Save();
}