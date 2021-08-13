using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data {
	public class LevelTileSet : TileSet {
		protected LevelFile Level { get; }

		public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
			Level = level;
			foreach (var tile in level.Tiles) {
				
			}
		}
	}
}