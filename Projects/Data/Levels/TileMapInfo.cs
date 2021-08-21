using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Projects.Data.Levels {
	public class TileMapInfo {
		protected TileMapData TileMapData { get; }
		protected AreaInfo AreaInfo { get; }

		public TileMapInfo(TileMapData tileMapData, AreaInfo areaInfo) {
			TileMapData = tileMapData;
			AreaInfo = areaInfo;
		}

		protected LevelTileSet TileSet => AreaInfo.LevelTileSet;

		public Vector2 Position => new Vector2(TileMapData.X, TileMapData.Y) + AreaInfo.TilePosition;
		public int TileIndex => TileSet.FindTileByName(TileMapData.Tile);
	}
}