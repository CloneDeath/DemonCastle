using System;
using System.Linq;
using DemonCastle.Game.Animations;
using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTile : Node2D, IDamageable, IEntityState {
	private readonly LevelInfo _level;
	private readonly TileInfo _tile;

	private readonly BaseEntityVariables _variables;
	private readonly GameAnimation _animation;
	private readonly EntityStateMachine _stateMachine;
	private StaticBody2D? Body { get; set; }
	private GameTileStairs? Stairs { get; set; }

	public GameTile(IGameState game, LevelInfo level, TileInfo tile, DebugState debug) {
		_level = level;
		_tile = tile;

		Name = nameof(GameTile);

		AddChild(new Sprite2D {
			Name = "TileTexture",
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false,
			Scale = tile.Size * level.TileSize / tile.Region.Size
		});

		AddChild(_animation = new GameAnimation(this, debug));
		_animation.SetAnimation(tile.Animations);

		AddChild(_stateMachine = new EntityStateMachine(game, this, tile.InitialState, tile.States));
		_variables = new BaseEntityVariables(tile);

		SetupCollisions(debug);
		SetupStairs(debug);
	}

	private void SetupCollisions(DebugState debug) {
		if (!_tile.Collision.Any()) return;
		AddChild(Body = new StaticBody2D {
			Name = "CollisionBody",
			CollisionLayer = (uint)CollisionLayers.World
		});
		Body.AddChild(new CollisionShape2D {
			Shape = new ConvexPolygonShape2D {
				Points = _tile.Collision.Select(v => v * _level.TileSize * _tile.Size).ToArray()
			},
			DebugColor = new Color(Colors.Aqua, 0.5f),
			Visible = debug.ShowCollisions
		});
	}

	private void SetupStairs(DebugState debug) {
		if (_tile.Stairs == null) return;

		AddChild(Stairs = new GameTileStairs(_level, _tile, _tile.Stairs, debug));
	}

	public Guid Id { get; } = Guid.NewGuid();
	public void TakeDamage(int amount) {}

	private int _facing = 1;
	public int Facing {
		get => _facing;
		set => _facing = value >= 0 ? 1 : -1;
	}

	protected Vector2 _moveDirection = Vector2.Zero;

	#region IEntityState
	public void SetFacing(int facing) => Facing = facing;
	public Vector2 AreaPosition => Position;
	public bool WasKilled => false;
	public IVariables Variables => _variables;

	public void SetAnimation(Guid animationId) => _animation.Play(animationId);
	public void ChangeStateTo(Guid stateId) => _stateMachine.ChangeState(stateId);
	public void Despawn() {
		_animation.PlayNone();
		_stateMachine.ChangeState(Guid.Empty);
	}
	#endregion

	public void MoveRight() => _moveDirection = Vector2.Right;
	public void MoveLeft() => _moveDirection = Vector2.Left;
	public void MoveForward() => _moveDirection = new Vector2(Facing, 0);
	public void MoveBackward() => _moveDirection = new Vector2(-Facing, 0);
	public void StopMoving() => _moveDirection = Vector2.Zero;
}