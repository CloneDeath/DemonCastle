using System;
using DemonCastle.Game.States;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer : CharacterBody2D {
	public float WalkSpeed => Character.WalkSpeed * Level.TileSize.X;
	public float Gravity => Character.Gravity * Level.TileSize.Y;
	public float JumpHeight => Character.JumpHeight * Level.TileSize.Y;
	public int Facing { get; set; } = 1;

	private IState State = new NormalState();

	private int _moveDirection;
	private bool _jump;
	public bool ApplyGravity { get; set; } = true;

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
		}

		Velocity = new Vector2(_moveDirection * WalkSpeed, _jump ? -GetJumpSpeed() : Velocity.Y);
		_jump = false;
		if (ApplyGravity) {
			Velocity = new Vector2(Velocity.X, (float)(Velocity.Y + Gravity * delta));
		}
		MoveAndSlide();

		Animation.Scale = new Vector2(Facing, 1);
	}

	private float GetJumpSpeed() {
		var time = Mathf.Sqrt(JumpHeight * 2 / Gravity);
		return time * Gravity;
	}

	public void MoveRight() => _moveDirection = 1;
	public void MoveLeft() => _moveDirection = -1;
	public void StopMoving() => _moveDirection = 0;

	public void MoveTo(Vector2 target) {
		var direction = target - GlobalPosition;
		_moveDirection = Math.Sign(direction.X);
	}

	public void Jump() {
		_jump = true;
	}
}