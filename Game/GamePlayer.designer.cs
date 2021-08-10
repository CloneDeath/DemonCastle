using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer {
		protected CharacterInfo Character { get; }
		public GamePlayer(CharacterInfo character) {
			Character = character;
			AddChild(new ColorRect {
				RectSize = new Vector2(16, 16),
				RectPosition = new Vector2(-8, -16)
			});
		}
	}
}