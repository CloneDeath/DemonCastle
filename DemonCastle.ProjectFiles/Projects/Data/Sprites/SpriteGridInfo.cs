using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public class SpriteGridInfo : FileInfo<SpriteGridFile>, ISpriteInfo {
		public Texture Texture => File.GetTexture(Resource.File);
		
		public SpriteGridInfo(FileNavigator<SpriteGridFile> file) : base(file) { }

		
		protected Vector2 Offset => new Vector2(Resource.XOffset, Resource.YOffset);
		protected Vector2 Span => new Vector2(Resource.Width + Resource.XSeparation, Resource.Height + Resource.YSeparation);
		protected Vector2 Size => new Vector2(Resource.Width, Resource.Height);
		
		public string SpriteFile {
			get => Resource.File;
			set { Resource.File = value; Save(); }
		}

		public int Width {
			get => Resource.Width;
			set { Resource.Width = value; Save(); }
		}

		public int Height {
			get => Resource.Height;
			set { Resource.Height = value; Save(); }
		}

		public int XOffset {
			get => Resource.XOffset;
			set { Resource.XOffset = value; Save(); }
		}

		public int YOffset {
			get => Resource.YOffset;
			set { Resource.YOffset = value; Save(); }
		}

		public int XSeparation {
			get => Resource.XSeparation;
			set { Resource.XSeparation = value; Save(); }
		}

		public int YSeparation {
			get => Resource.YSeparation;
			set { Resource.YSeparation = value; Save(); }
		}
		
		public SpriteInfoNode GetSprite(string spriteName) {
			var spriteData = GetSpriteData(spriteName);
			var region = GetSpriteRegion(spriteData);
			return new SpriteInfoNode(Texture, new SpriteDefinition {
				Region = region,
				FlipHorizontal = spriteData.FlipHorizontal
			});
		}

		public Rect2 GetRegion(string spriteName) {
			var spriteData = GetSpriteData(spriteName);
			return GetSpriteRegion(spriteData);
		}

		private Rect2 GetSpriteRegion(SpriteGridData spriteGridData) {
			return new Rect2 {
				Position = Offset + Span * new Vector2(spriteGridData.X, spriteGridData.Y),
				Size = Size
			};
		}

		protected SpriteGridData GetSpriteData(string spriteName) => Resource.Sprites.First(s => s.Name == spriteName);
	}
}