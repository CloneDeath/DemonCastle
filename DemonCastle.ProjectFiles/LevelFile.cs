using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class LevelFile {
		public string Name { get; set; } = string.Empty;
		public int TileWidth { get; set; } = 16;
		public int TileHeight { get; set; } = 16;
		public int AreaWidth { get; set; } = 16;
		public int AreaHeight { get; set; } = 9;
		public List<TileData> Tiles { get; set; } = new List<TileData>();
		public List<AreaData> Areas { get; set; } = new List<AreaData>();
	}

	public class TileData {
		public string Name { get; set; } = string.Empty;
		public bool Solid { get; set; } = true;
		public List<FrameData> Frames { get; set; } = new List<FrameData>();
	}

	public class AreaData {
		public string Name { get; set; } = string.Empty;
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public int Width { get; set; } = 1;
		public int Height { get; set; } = 1;
		public List<TileMapData> TileMap { get; set; } = new List<TileMapData>();
	}

	public class TileMapData {
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public string Tile { get; set; }
	}
}