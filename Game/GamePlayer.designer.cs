using DemonCastle.Game.Animations;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer {
		protected LevelInfo Level { get; }
		protected CharacterInfo Character { get; }
		
		protected PlayerAnimation Animation { get; }
		
		public GamePlayer(LevelInfo level, CharacterInfo character) {
			Level = level;
			Character = character;
			AddChild(Animation = new PlayerAnimation(character));
		}
	}
}