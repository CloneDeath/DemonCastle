using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer {
		protected LevelInfo Level { get; }
		protected CharacterInfo Character { get; }
		
		public GamePlayer(LevelInfo level, CharacterInfo character) {
			Level = level;
			Character = character;
			AddChild(new ColorRect {
				RectSize = new Vector2(16, 16),
				RectPosition = new Vector2(-8, -16)
			});
		}
	}
}