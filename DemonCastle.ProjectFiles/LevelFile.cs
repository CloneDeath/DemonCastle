using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class LevelFile {
		public string Name { get; set; } = string.Empty;
		public int AreaWidth { get; set; } = 16;
		public int AreaHeight { get; set; } = 9;
		public List<TileInfo> Tiles { get; set; } = new List<TileInfo>();
		public List<AreaInfo> Areas { get; set; } = new List<AreaInfo>();
	}

	public class TileInfo {
		public string Name { get; set; } = string.Empty;
		public bool Solid { get; set; } = true;
		public List<FrameInfo> Frames { get; set; } = new List<FrameInfo>();
	}

	public class AreaInfo {
		public string Name { get; set; } = string.Empty;
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public int Width { get; set; } = 1;
		public int Height { get; set; } = 1;
		public List<TileMapInfo> TileMap { get; set; } = new List<TileMapInfo>();
	}

	public class TileMapInfo {
		public int X { get; set; } = 0;
		public int Y { get; set; } = 0;
		public string Tile { get; set; }
	}
}