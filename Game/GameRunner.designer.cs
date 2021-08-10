using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game {
	public partial class GameRunner {
		protected GameLevel Level { get; }
		protected GamePlayer Player { get; }
		public GameRunner(LevelInfo level, CharacterInfo player) {
			AddChild(Level = new GameLevel(level));
			AddChild(Player = new GamePlayer(level, player) {
				Position = new Vector2(300, 300)
			});
		}
	}
}