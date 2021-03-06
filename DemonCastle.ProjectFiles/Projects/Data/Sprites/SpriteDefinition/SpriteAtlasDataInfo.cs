using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition {
	public class SpriteAtlasDataInfo : ISpriteDefinition {
		protected SpriteAtlasInfo SpriteAtlasInfo { get; }
		protected SpriteAtlasData Data { get; }

		public SpriteAtlasDataInfo(SpriteAtlasInfo spriteAtlasInfo, SpriteAtlasData data) {
			SpriteAtlasInfo = spriteAtlasInfo;
			Data = data;
		}

		public string Name => Data.Name;
		public Texture Texture => SpriteAtlasInfo.Texture;

		public Rect2 Region => new Rect2 {
			Position = new Vector2(Data.X, Data.Y),
			Size = new Vector2(Data.Width, Data.Height)
		};

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
}