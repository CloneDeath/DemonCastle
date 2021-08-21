using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Data.Sprites;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data.Levels {
	public class TileInfo {
		public int Index { get; }
		protected TileData TileData { get; }
		protected FileNavigator<LevelFile> Level { get; }

		public TileInfo(FileNavigator<LevelFile> level, TileData tileData, int index) {
			Index = index;
			TileData = tileData;
			Level = level;
		}
		
		public string Name => TileData.Name;
		protected ISpriteInfo Sprite => Level.GetSprite(TileData.Source);
		public Texture Texture => Sprite.Texture;
		public Rect2 Region => Sprite.GetRegion(TileData.Sprite);
		public Vector2[] Collision => TileData.Collision.Select(c => new Vector2(c.X, c.Y)).ToArray();
	}
}