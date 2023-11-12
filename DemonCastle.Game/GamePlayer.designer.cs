using DemonCastle.Game.Animations;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer {
	protected IGameLogger Logger { get; }
	protected LevelInfo Level { get; }
	protected CharacterInfo Character { get; }

	public PlayerAnimation Animation { get; }
	public Area2D StairsDetection { get; }
	public Area2D FloorDetection { get; }

	public GamePlayer(LevelInfo level, CharacterInfo character, IGameLogger logger) {
		Level = level;
		Character = character;
		Logger = logger;

		AddChild(new CollisionShape2D {
			Position = new Vector2(0, -Character.Size.Y/2),
			Shape = new RectangleShape2D {
				Size = Character.Size
			}
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;
		AddChild(Animation = new PlayerAnimation(character));

		AddChild(StairsDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World,
			Monitoring = true
		});
		StairsDetection.AddChild(new CollisionShape2D {
			Shape = new RectangleShape2D {
				Size = new Vector2(level.TileSize.X * 3, level.TileSize.Y / 2)
			}
		});

		AddChild(FloorDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World
		});
		FloorDetection.AddChild(new CollisionShape2D {
			Shape = new RectangleShape2D {
				Size = new Vector2(Character.Size.X, 2)
			}
		});
	}
}