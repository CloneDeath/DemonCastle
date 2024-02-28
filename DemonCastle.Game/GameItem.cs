using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameItem : GameBaseEntity {
	private readonly ItemInfo _item;
	private readonly Area2D _playerDetector;

	public GameItem(IGameState game, LevelInfo level, ItemInfo item, IGameLogger logger, DebugState debug) : base(game,
		level, item, logger, debug) {
		_item = item;
		Name = $"{nameof(GameItem)} {item.Name} ({Id})";

		AddChild(_playerDetector = new Area2D {
			CollisionLayer = (uint) CollisionLayers.World,
			CollisionMask = (uint) CollisionLayers.Player,
			Monitoring = true
		});
		_playerDetector.AddChild(new CollisionShape2D {
			Shape = new RectangleShape2D {
				Size = item.Size
			},
			DebugColor = new Color(Colors.Purple, 0.5f),
			Visible = debug.ShowCollisions
		});
		_playerDetector.BodyEntered += PlayerDetector_OnBodyEntered;

	}

	private void PlayerDetector_OnBodyEntered(Node2D body) {
		if (body is not GamePlayer) return;
		_item.OnPickup.Execute(Game);
		QueueFree();
	}

	protected override bool IsImmobile => false;
	public override bool WasKilled => false;

	public override float MoveSpeed => _item.MoveSpeed * Level.TileSize.X;
	public override float JumpHeight => 0 * Level.TileSize.Y;
	protected override float Gravity => _item.Gravity * Level.TileSize.Y;
	protected override bool ApplyDamage(int amount) => false;
}