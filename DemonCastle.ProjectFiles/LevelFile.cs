using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class LevelFile {
		public string Name { get; set; } = string.Empty;
		public int TileWidth { get; set; } = 16;
		public int TileHeight { get; set; } = 16;
		public int AreaWidth { get; set; } = 16;
		public int AreaHeight { get; set; } = 9;
		public StartingData StartingPosition { get; set; } = new();
		public List<TileData> Tiles { get; set; } = new();
		public List<AreaData> Areas { get; set; } = new();
	}

	public class StartingData {
		public string Area3D { get; set; } = string.Empty;
		public float X { get; set; }
		public float Y { get; set; }
	}

	public class TileData {
		public string Name { get; set; } = string.Empty;
		public string Source { get; set; } = string.Empty;
		public string Sprite2D { get; set; } = string.Empty;
		public List<CollisionData> Collision { get; set; } = new();
	}

	public class CollisionData {
		public float X { get; set; }
		public float Y { get; set; }
	}

	public class AreaData {
		public string Name { get; set; } = string.Empty;
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public int Width { get; set; } = 1;
		public int Height { get; set; } = 1;
		public List<TileMapData> TileMap { get; set; } = new();
	}

	public class TileMapData {
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public string Tile { get; set; }
	}
}