using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public class LevelTileSet {
		protected LevelFile Level { get; }
		protected FileNavigator<LevelFile> File { get; }

		private readonly Dictionary<string, TileInfo> _tileInfos = new();
		
		public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
			File = file;
			Level = level;
			foreach (var tile in level.Tiles) {
				_tileInfos[tile.Name] = new TileInfo(file, tile);
			}
		}

		public TileInfo GetTileInfo(string tileName) {
			return _tileInfos[tileName];
		}
	}
}