using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.States;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer : CharacterBody2D {
	private IFrameInfo? PreviousFrame;

	public float WalkSpeed => Character.WalkSpeed * Level.TileSize.X;
	public float Gravity => Character.Gravity * Level.TileSize.Y;
	public float JumpHeight => Character.JumpHeight * Level.TileSize.Y;
	public int Facing { get; set; } = 1;

	private IState State = new NormalState();

	private Vector2 _moveDirection;
	private bool _jump;
	private bool _applyGravity = true;

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

		UpdateWeaponFrame();

		PreviousFrame = Animation.CurrentFrame ?? null;

		if (_applyGravity) {
			Velocity += new Vector2(0, (float)(Gravity * delta));
			Velocity = new Vector2(_moveDirection.X * WalkSpeed, _jump ? -GetJumpSpeed() : Velocity.Y);
		} else {
			Velocity = _moveDirection * WalkSpeed;
		}

		StopMoving();
		_jump = false;

		MoveAndSlide();

		Animation.Scale = new Vector2(Facing, 1);
	}

	private void UpdateWeaponFrame() {
		if (Animation.CurrentFrame == null || Animation.CurrentFrame == PreviousFrame) {
			return;
		}

		var characterFrame = Animation.CurrentFrame;
		if (characterFrame == null) return;

		var weaponSlot = characterFrame.Slots.FirstOrDefault(s => s.Name == "Weapon");

		if (weaponSlot == null) {
			Weapon.PlayNone();
		}
		else {
			Weapon.Play(weaponSlot.Animation);
		}
		var weaponFrame = Weapon.CurrentFrame;

		var characterFrameTopLeftOffset = characterFrame.SpriteDefinition.Region.Size * new Vector2(0.5f, 1);
		var weaponFrameTopLeftOffset = weaponFrame?.SpriteDefinition.Region.Size * new Vector2(0.5f, 1) ?? Vector2.Zero;
		Weapon.Position = (weaponSlot?.Position ?? Vector2.Zero) - characterFrameTopLeftOffset
						+ weaponFrameTopLeftOffset - (weaponFrame?.Offset ?? Vector2.Zero);
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

	public IEnumerable<Tiles.GameTileStairs> GetNearbyStairs() {
		return StairsDetection.GetOverlappingAreas().Where(a => a is Tiles.GameTileStairs).Cast<Tiles.GameTileStairs>();
	}

	public void Jump() {
		_jump = true;
	}

	public void EnableWorldCollisions() {
		CollisionMask = (uint)CollisionLayers.World;
	}

	public void DisableWorldCollisions() {
		CollisionMask = 0;
	}

	public void EnableGravity() {
		if (_applyGravity) return;

		Velocity = Vector2.Zero;
		_applyGravity = true;
	}

	public void DisableGravity() {
		if (!_applyGravity) return;

		Velocity = Vector2.Zero;
		_applyGravity = false;
	}

	public bool IsStandingOnFloor() {
		return FloorDetection.GetOverlappingBodies().Any();
	}
}