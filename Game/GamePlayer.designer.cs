using DemonCastle.Projects.Data;

namespace DemonCastle.Game {
	public partial class GamePlayer {
		protected CharacterInfo Character { get; }
		public GamePlayer(CharacterInfo character) {
			Character = character;
		}
	}
}