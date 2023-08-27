using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public class TileInfo {
		protected TileData TileData { get; }
		protected FileNavigator<LevelFile> Level { get; }
		protected Vector2I LevelTileSize => new(Level.Resource.TileWidth, Level.Resource.TileHeight);

		public TileInfo(FileNavigator<LevelFile> level, TileData tileData) {
			TileData = tileData;
			Level = level;
		}
		
		public string Name => TileData.Name;
		protected ISpriteSource Source => Level.GetSprite(TileData.Source);
		protected ISpriteDefinition Sprite => Source.GetSpriteDefinition(TileData.Sprite);
		public Texture2D Texture => Sprite.Texture;
		public string TextureName => TileData.Source;
		public Rect2 Region => Sprite.Region;
		public Vector2[] Collision => TileData.Collision.Select(c => new Vector2(c.X, c.Y) * LevelTileSize).ToArray();
		public bool FlipHorizontal => Sprite.FlipHorizontal;
	}
}