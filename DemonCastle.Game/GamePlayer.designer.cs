using DemonCastle.Game.Animations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
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
				Position = new Vector2(0, -Character.Size.y/2),
				Shape = new RectangleShape2D {
					Extents = Character.Size / 2
				}
			});
			CollisionLayer = (uint) CollisionLayers.Player;
			AddChild(Animation = new PlayerAnimation(character));
		}
	}
}