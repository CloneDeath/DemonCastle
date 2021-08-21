using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Game {
	public partial class GameLevel {
		protected LevelInfo Level { get; }
		
		public GameLevel(LevelInfo level) {
			Level = level;
			TileSet = level.TileSet;
			CellSize = level.TileSize;
			CollisionLayer = (uint) CollisionLayers.World;

			LoadLevel();
		}
	}
}