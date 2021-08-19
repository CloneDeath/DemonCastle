using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;
using Godot.Collections;

namespace DemonCastle.Projects.Data {
	public class LevelTileSet : TileSet {
		protected LevelFile Level { get; }
		protected FileNavigator<LevelFile> File { get; }
		
		public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
			File = file;
			Level = level;
			foreach (var tile in level.Tiles) {
				var index = Tiles.Count;
				var tileInfo = new TileInfo(file, tile, index);
				Tiles[index] = tileInfo;
				RegisterTile(tileInfo);
			}
		}

		protected Dictionary<int, TileInfo> Tiles { get; } = new Dictionary<int, TileInfo>();
		
		private void RegisterTile(TileInfo tileInfo) {
			CreateTile(tileInfo.Index);
			TileSetName(tileInfo.Index, tileInfo.Name);
			TileSetTexture(tileInfo.Index, tileInfo.Texture);
			TileSetRegion(tileInfo.Index, tileInfo.Region);
		}
	}
}