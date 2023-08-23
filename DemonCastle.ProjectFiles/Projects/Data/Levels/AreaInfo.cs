using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public class AreaInfo {
		protected AreaData Area3D { get; }
		public LevelInfo LevelInfo { get; }

		public AreaInfo(AreaData area, LevelInfo levelInfo) {
			Area3D = area;
			LevelInfo = levelInfo;
		}

		public LevelTileSet LevelTileSet => LevelInfo.LevelTileSet;

		public IEnumerable<TileMapInfo> TileMap => Area3D.TileMap.Select(tm => new TileMapInfo(tm, this));
		public Vector2 AreaPosition => new(Area3D.X, Area3D.Y);
		public Vector2 TilePosition => LevelInfo.TileSize * AreaPosition;
	}
}