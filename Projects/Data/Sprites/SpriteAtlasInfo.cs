using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data.Sprites {
	public class SpriteAtlasInfo : ISpriteInfo {
		protected FileNavigator<SpriteAtlasFile> File { get; }
		protected SpriteAtlasFile Sprite => File.Resource;

		public Texture Texture => File.GetTexture(Sprite.File);

		public SpriteAtlasInfo(FileNavigator<SpriteAtlasFile> file) {
			File = file;
		}
		public SpriteInfoNode GetSprite(string spriteName) {
			var spriteData = GetSpriteData(spriteName);
			var region = GetSpriteRegion(spriteData);
			return new SpriteInfoNode(Texture, region, spriteData.FlipHorizontal);
		}

		public Rect2 GetRegion(string spriteName) {
			var spriteData = GetSpriteData(spriteName);
			return GetSpriteRegion(spriteData);
		}

		protected Rect2 GetSpriteRegion(SpriteAtlasData spriteData) {
			return new Rect2 {
				Position = new Vector2(spriteData.X, spriteData.Y),
				Size = new Vector2(spriteData.Width, spriteData.Height)
			};
		}

		protected SpriteAtlasData GetSpriteData(string spriteName) => Sprite.Sprites.First(s => s.Name == spriteName);
	}
}