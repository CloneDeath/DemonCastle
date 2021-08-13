using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Projects.Data {
	public class LevelTileSet : TileSet {
		private LevelFile _level;

		public LevelTileSet(LevelFile level) {
			_level = level;
		}
	}
}