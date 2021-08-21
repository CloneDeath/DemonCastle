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
			AddChild(new CollisionShape2D {
				Shape = new RectangleShape2D {
					Extents = Character.Size / 2
				}
			});
			CollisionLayer = (uint) CollisionLayers.Player;
			AddChild(Animation = new PlayerAnimation(character));
		}
	}
}