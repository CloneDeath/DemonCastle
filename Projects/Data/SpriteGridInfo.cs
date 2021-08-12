using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data {
	public class SpriteGridInfo : ISpriteInfo {
		protected FileNavigator<SpriteGridFile> File { get; }
		protected SpriteGridFile Sprite => File.Resource;
		protected Texture Texture => File.GetTexture(Sprite.File);

		public SpriteGridInfo(FileNavigator<SpriteGridFile> file) {
			File = file;
		}

		protected Vector2 Offset => new Vector2(Sprite.XOffset, Sprite.YOffset);
		protected Vector2 Span => new Vector2(Sprite.Width + Sprite.XSeparation, Sprite.Height + Sprite.YSeparation);
		protected Vector2 Size => new Vector2(Sprite.Width, Sprite.Height);
		
		public SpriteInfoNode GetSprite(string spriteName) {
			return new SpriteInfoNode(Texture, GetSpriteRegion(spriteName));
		}

		public Rect2 GetSpriteRegion(string spriteName) => GetSpriteRegion(GetSpriteData(spriteName));

		private Rect2 GetSpriteRegion(SpriteData spriteData) {
			return new Rect2 {
				Position = Offset + Span * new Vector2(spriteData.X, spriteData.Y),
				Size = Size
			};
		}

		protected SpriteData GetSpriteData(string spriteName) => Sprite.Sprites.First(s => s.Name == spriteName);
	}
}