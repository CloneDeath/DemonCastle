using DemonCastle.Game.Animations;
using DemonCastle.ProjectFiles;
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
				Position = new Vector2(0, -Character.Size.Y/2),
				Shape = new RectangleShape2D {
					Size = Character.Size / 2
				}
			});
			CollisionLayer = (uint) CollisionLayers.Player;
			CollisionMask = (uint)CollisionLayers.World;
			AddChild(Animation = new PlayerAnimation(character));
		}
	}
}