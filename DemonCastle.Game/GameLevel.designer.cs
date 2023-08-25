using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Game {
	public partial class GameLevel {
		protected LevelInfo Level { get; }
		
		public GameLevel(LevelInfo level) {
			Level = level;
			LoadLevel();
		}
	}
}