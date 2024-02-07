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

	protected Vector2 MoveDirection = Vector2.Zero;

	protected HitInvulnerabilityTracker InvulnerabilityTracker { get; }

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

		AddChild(InvulnerabilityTracker = new HitInvulnerabilityTracker());
		AddChild(new DebugPosition2D(debug));
	}

	public void TakeDamage(int amount) {
		if (InvulnerabilityTracker.IsInvulnerable) return;
		if (ApplyDamage(amount)) {
			InvulnerabilityTracker.EntityHasTakenDamage();
		}
	}

	protected abstract bool ApplyDamage(int amount);

	public void MoveRight() => MoveDirection = Vector2.Right;
	public void MoveLeft() => MoveDirection = Vector2.Left;
	public void MoveForward() => MoveDirection = new Vector2(Facing, 0);
	public void MoveBackward() => MoveDirection = new Vector2(-Facing, 0);
	public void StopMoving() => MoveDirection = Vector2.Zero;

	public void EnableWorldCollisions() => CollisionMask = (uint)CollisionLayers.World;
	public void DisableWorldCollisions() => CollisionMask = 0;

	protected abstract void AlignAnimationNodes();
}