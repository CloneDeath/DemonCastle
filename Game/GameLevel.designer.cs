using DemonCastle.Projects.Data;

namespace DemonCastle.Game {
	public partial class GameLevel {
		protected LevelInfo Level { get; }
		
		public GameLevel(LevelInfo level) {
			Level = level;
			TileSet = level.TileSet;
		}
	}
}