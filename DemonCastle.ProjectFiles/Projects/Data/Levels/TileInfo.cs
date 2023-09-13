using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public class TileInfo {
		protected TileData TileData { get; }
		protected FileNavigator<LevelFile> Level { get; }
		public Vector2I TileSize => new(Level.Resource.TileWidth, Level.Resource.TileHeight);
		
		public string Directory => Level.Directory;

		public TileInfo(FileNavigator<LevelFile> level, TileData tileData) {
			TileData = tileData;
			Level = level;
		}

		public string Name {
			get => TileData.Name;
			set { TileData.Name = value; Save(); }
		}

		public string SourceFile {
			get => TileData.Source;
			set { TileData.Source = value; Save(); }
		}

		public string SpriteName {
			get => TileData.Sprite;
			set { TileData.Sprite = value; Save(); }
		}

		protected ISpriteSource Source => Level.GetSprite(SourceFile);
		protected ISpriteDefinition Sprite => Source.GetSpriteDefinition(SpriteName);
		public Texture2D Texture => Sprite.Texture;
		public Rect2 Region => Sprite.Region;
		public Vector2[] Collision => TileData.Collision.Select(c => new Vector2(c.X, c.Y) * TileSize).ToArray();
		public bool FlipHorizontal => Sprite.FlipHorizontal;

		private void Save() => Level.Save();
	}
}