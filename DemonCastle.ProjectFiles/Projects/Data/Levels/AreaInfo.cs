using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public class AreaInfo {
		protected AreaData Area { get; }
		public LevelInfo LevelInfo { get; }

		public AreaInfo(AreaData area, LevelInfo levelInfo) {
			Area = area;
			LevelInfo = levelInfo;
		}

		public LevelTileSet LevelTileSet => LevelInfo.LevelTileSet;

		public IEnumerable<TileMapInfo> TileMap => Area.TileMap.Select(tm => new TileMapInfo(tm, this));
		public Vector2 AreaPosition => new(Area.X, Area.Y);
		public Vector2 TilePosition => LevelInfo.TileSize * AreaPosition;
	}
}