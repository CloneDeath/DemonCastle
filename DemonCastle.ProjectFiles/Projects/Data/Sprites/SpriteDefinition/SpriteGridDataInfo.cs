using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition {
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
		public Texture2D Texture2D => SpriteGrid.Texture2D;
		public Rect2 Region => new() {
			Position = Offset + Span * new Vector2(Data.X, Data.Y),
			Size = Size
		};

		public bool FlipHorizontal {
			get => Data.FlipHorizontal;
			set { Data.FlipHorizontal = value; Save(); }
		}
		public Color TransparentColor => Colors.Transparent;
		public float TransparentColorThreshold => 0.001f;

		protected Vector2 Offset => SpriteGrid.Offset;
		protected Vector2 Span => SpriteGrid.Span;
		protected Vector2 Size => SpriteGrid.Size;

		public void Save() => SpriteGrid.Save();
		public void Remove() => SpriteGrid.Remove(Data, this);
	}
}