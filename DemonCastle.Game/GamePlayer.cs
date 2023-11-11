using System.Collections.Generic;
using DemonCastle.Game.States;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer : CharacterBody2D {
	public float WalkSpeed => Character.WalkSpeed * Level.TileSize.X;
	public float Gravity => Character.Gravity * Level.TileSize.Y;
	public float JumpHeight => Character.JumpHeight * Level.TileSize.Y;
	public int Facing { get; set; } = 1;

	private IState State = new NormalState();

	private Vector2 _momentum;
	private Vector2 _moveDirection;
	private bool _jump;
	public bool ApplyGravity { get; set; } = true;
	private readonly List<GameTileStairs> _stairs = new();

	public override void _EnterTree() {
		base._EnterTree();
		State.OnEnter(this);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var nextState = State.Update(this, delta);
		if (nextState != null) {
			State.OnExit(this);
			State = nextState;
			State.OnEnter(this);
			Logger.StateChanged(State);
		}

		if (ApplyGravity) {
			_momentum = new Vector2(_momentum.X, (float)(_momentum.Y + Gravity * delta));
		}
		Velocity = _momentum;
		if (_moveDirection.Length() > 0) {
			Velocity += _moveDirection.Normalized() * WalkSpeed;
		}
		if (_jump) {
			Velocity = new Vector2(Velocity.X, -GetJumpSpeed());
		}
		StopMoving();
		_jump = false;

		MoveAndSlide();
		if (Velocity.Y == 0) {
			_momentum.Y = 0;
		}
		if (Velocity.X == 0) {
			_momentum.X = 0;
		}

		Animation.Scale = new Vector2(Facing, 1);
	}

	private float GetJumpSpeed() {
		var time = Mathf.Sqrt(JumpHeight * 2 / Gravity);
		return time * Gravity;
	}

	public void MoveRight() => _moveDirection = Vector2.Right;
	public void MoveLeft() => _moveDirection = Vector2.Left;
	public void StopMoving() => _moveDirection = Vector2.Zero;

	public void MoveTowards(Vector2 target) {
		var direction = target - GlobalPosition;
		_moveDirection = direction.Normalized();
	}

	public IEnumerable<GameTileStairs> GetNearbyStairs() => _stairs;

	public void Jump() {
		_jump = true;
	}

	private void StairsDetection_OnAreaEntered(Area2D area) {
		if (area is not GameTileStairs stairs) return;
		if (_stairs.Contains(stairs)) return;
		_stairs.Add(stairs);
	}

	private void StairsDetection_OnAreaExited(Area2D area) {
		if (area is not GameTileStairs stairs) return;
		if (!_stairs.Contains(stairs)) return;
		_stairs.Remove(stairs);
	}

	public void DisableWorldCollisions() {
		CollisionMask = 0;
	}

	public void EnableWorldCollisions() {
		CollisionMask = (uint)CollisionLayers.World;
	}
}