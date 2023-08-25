using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels {
	public partial class LevelTileSet : TileSet {
		protected LevelFile Level { get; }
		protected FileNavigator<LevelFile> File { get; }
		
		public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
			File = file;
			Level = level;
			TileSize = new Vector2I(level.TileWidth, level.TileHeight);
			foreach (var tile in level.Tiles) {
				var tileInfo = new TileInfo(file, tile);
				Tiles[tile.Name] = tileInfo;
				RegisterTile(tileInfo);
			}
		}

		protected Dictionary<string, TileInfo> Tiles { get; } = new();
		protected Dictionary<string, SourceMetadata> Sources { get; } = new();
		
		private void RegisterTile(TileInfo tileInfo) {
			var source = CreateOrFindSource(tileInfo);
			source.CreateTile(tileInfo);
		}

		private SourceMetadata CreateOrFindSource(TileInfo tileInfo) {
			if (Sources.TryGetValue(tileInfo.TextureName, out var existing)) {
				return existing;
			}
			
			var source = new TileSetAtlasSource();
			var sourceId = AddSource(source);
			source.Texture = tileInfo.Texture;
			source.TextureRegionSize = TileSize;

			var metadata = new SourceMetadata(source, sourceId);
			Sources[tileInfo.TextureName] = metadata;
			return metadata;
		}

		public int GetTileSourceId(string tileName) {
			var tile = Tiles[tileName];
			var source = Sources[tile.TextureName];
			return source.SourceId;
		}

		public Vector2I GetTileAtlasCoords(string tile) {
			var tileInfo = Tiles.TryGetValue(tile, out var data) ? data : throw new KeyNotFoundException();
			return tileInfo.AtlasCoords;
		}
	}
}