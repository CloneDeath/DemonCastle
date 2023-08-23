using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
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
		protected ISpriteSource Source => Level.GetSprite(TileData.Source);
		protected ISpriteDefinition Sprite2D => Source.GetSpriteDefinition(TileData.Sprite2D);
		public Texture2D Texture2D => Sprite2D.Texture2D;
		public Rect2 Region => Sprite2D.Region;
		public Vector2[] Collision => TileData.Collision.Select(c => new Vector2(c.X, c.Y)).ToArray();
	}
}