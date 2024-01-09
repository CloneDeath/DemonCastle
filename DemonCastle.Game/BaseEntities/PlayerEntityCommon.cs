using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public abstract partial class PlayerEntityCommon : CharacterBody2D, IDamageable {
	public Guid Id { get; } = Guid.NewGuid();

	protected readonly IGameState Game;
	protected readonly IGameLogger Logger;

	protected CollisionShape2D CollisionShape { get; }

	public abstract float MoveSpeed { get; }
	protected abstract float Gravity { get; }

	private int _facing = 1;
	public int Facing {
		get => _facing;
		set => _facing = value >= 0 ? 1 : -1;
	}

	protected Vector2 _moveDirection = Vector2.Zero;

	protected PlayerEntityCommon(IGameState game, IGameLogger logger, DebugState debug) {
		Game = game;
		Logger = logger;

		Name = nameof(PlayerEntityCommon);

		AddChild(CollisionShape = new CollisionShape2D {
			DebugColor = new Color(Colors.Green, 0.5f),
			Visible = debug.ShowCollisions
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;

		AddChild(new DebugPosition2D(debug));
	}

	public virtual void TakeDamage(int amount) {

	}

	public void MoveRight() => _moveDirection = Vector2.Right;
	public void MoveLeft() => _moveDirection = Vector2.Left;
	public void MoveForward() => _moveDirection = new Vector2(Facing, 0);
	public void MoveBackward() => _moveDirection = new Vector2(-Facing, 0);
	public void StopMoving() => _moveDirection = Vector2.Zero;

	protected abstract void AlignAnimationNodes();
}