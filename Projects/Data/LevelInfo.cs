using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data {
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
		public Vector2 TileSize => new Vector2(Level.TileWidth, Level.TileHeight);
		public TileSet TileSet => LevelTileSet;

		public IEnumerable<AreaInfo> Areas => Level.Areas.Select(area => new AreaInfo(area, this));
	}
}