using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public class SpriteGridInfo : ISpriteInfo {
		protected FileNavigator<SpriteGridFile> File { get; }
		protected SpriteGridFile Sprite => File.Resource;
		public Texture Texture => File.GetTexture(Sprite.File);

		public SpriteGridInfo(FileNavigator<SpriteGridFile> file) {
			File = file;
		}

		protected Vector2 Offset => new Vector2(Sprite.XOffset, Sprite.YOffset);
		protected Vector2 Span => new Vector2(Sprite.Width + Sprite.XSeparation, Sprite.Height + Sprite.YSeparation);
		protected Vector2 Size => new Vector2(Sprite.Width, Sprite.Height);
		
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

		protected SpriteGridData GetSpriteData(string spriteName) => Sprite.Sprites.First(s => s.Name == spriteName);
	}
}