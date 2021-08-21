using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data.Levels {
	public class LevelInfo : IListableInfo {
		protected FileNavigator<LevelFile> File { get; }
		protected LevelFile Level => File.Resource;
		public LevelTileSet LevelTileSet { get; }

		public LevelInfo(FileNavigator<LevelFile> file) {
			File = file;
			LevelTileSet = new LevelTileSet(Level, File);
		}
		
		public string Name => Level.Name;
		public int TileWidth => Level.TileWidth;
		public int TileHeight => Level.TileHeight;
		public Vector2 TileSize => new Vector2(Level.TileWidth, Level.TileHeight);
		public TileSet TileSet => LevelTileSet;

		public IEnumerable<AreaInfo> Areas => Level.Areas.Select(area => new AreaInfo(area, this));

		private AreaInfo GetAreaByName(string name) {
			var area = Level.Areas.First(a => a.Name == name);
			return new AreaInfo(area, this);
		}

		public Vector2 StartingLocation => TileSize * (
			GetAreaByName(Level.StartingPosition.Area).TilePosition
			+ new Vector2(Level.StartingPosition.X, Level.StartingPosition.Y)
		) + new Vector2(TileWidth/2f, TileHeight);
	}
}