using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer {
	protected IGameLogger Logger { get; }
	protected LevelInfo Level { get; }
	protected CharacterInfo Character { get; }

	public CharacterAnimation Animation { get; }
	public GameAnimation Weapon { get; }
	public Area2D StairsDetection { get; }
	public Area2D FloorDetection { get; }

	public GamePlayer(LevelInfo level, CharacterInfo character, DebugState debug, IGameLogger logger) {
		Level = level;
		Character = character;
		Logger = logger;

		AddChild(new CollisionShape2D {
			Position = new Vector2(0, -Character.Size.Y/2),
			Shape = new RectangleShape2D {
				Size = Character.Size
			},
			DebugColor = new Color(Colors.Green, 0.5f),
			Visible = debug.ShowCollisions
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;

		AddChild(Weapon = new GameAnimation(character.DefaultWeaponInfo.Animations, debug));
		AddChild(Animation = new CharacterAnimation(character, debug));

		AddChild(StairsDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World,
			Monitoring = true
		});
		StairsDetection.AddChild(new CollisionShape2D {
			Shape = new RectangleShape2D {
				Size = new Vector2(level.TileSize.X * 3, level.TileSize.Y / 2)
			},
			Visible = debug.ShowCollisions
		});

		AddChild(FloorDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World
		});
		FloorDetection.AddChild(new CollisionShape2D {
			Shape = new RectangleShape2D {
				Size = new Vector2(Character.Size.X, level.TileSize.Y / 4)
			},
			Visible = debug.ShowCollisions
		});

		AddChild(new DebugPosition2D(debug));
	}
}