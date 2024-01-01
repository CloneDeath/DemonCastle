using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.States;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer : CharacterBody2D, IDamageable {
	protected IGameLogger Logger { get; }
	protected LevelInfo? Level { get; set; }
	protected CharacterInfo? Character { get; set; }

	public CharacterAnimation Animation { get; }
	public GameAnimation Weapon { get; }
	public Area2D StairsDetection { get; }
	protected CollisionShape2D StairsShape { get; }
	public Area2D FloorDetection { get; }
	protected CollisionShape2D FloorShape { get; }
	protected CollisionShape2D CollisionShape { get; }

	private IFrameInfo? PreviousFrame;

	public float WalkSpeed => (Character?.WalkSpeed ?? 0) * (Level?.TileSize.X ?? 0);
	public float Gravity => (Character?.Gravity ?? 0) * (Level?.TileSize.Y ?? 0);
	public float JumpHeight => (Character?.JumpHeight ?? 0) * (Level?.TileSize.Y ?? 0);
	public int Facing { get; set; } = 1;

	private IState State = new NormalState();

	private Vector2 _moveDirection;
	private bool _jump;
	private bool _applyGravity = true;

	public GamePlayer(DebugState debug, IGameLogger logger) {
		Logger = logger;

		AddChild(CollisionShape = new CollisionShape2D {
			DebugColor = new Color(Colors.Green, 0.5f),
			Visible = debug.ShowCollisions
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;

		AddChild(Weapon = new GameAnimation(this, debug));
		AddChild(Animation = new CharacterAnimation(this, debug));

		AddChild(StairsDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World,
			Monitoring = true
		});
		StairsDetection.AddChild(StairsShape = new CollisionShape2D {
			DebugColor = new Color(Colors.Purple, 0.5f),
			Visible = debug.ShowCollisions
		});

		AddChild(FloorDetection = new Area2D {
			CollisionLayer = (uint) CollisionLayers.Player,
			CollisionMask = (uint) CollisionLayers.World
		});
		FloorDetection.AddChild(FloorShape = new CollisionShape2D {
			Visible = debug.ShowCollisions
		});

		AddChild(new DebugPosition2D(debug));
	}

	public Guid Id { get; } = Guid.NewGuid();

	public int Hp { get; set; } = 10;
	public int MaxHp => 10;

	public void TakeDamage(int amount) {
		Hp -= amount;
		if (Hp <= 0) {
			QueueFree();
		}
	}

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
		var characterFrame = Animation.CurrentFrame;
		if (characterFrame == null) return;
		if (characterFrame == PreviousFrame) return;

		var weaponSlot = characterFrame.Slots.FirstOrDefault(s => s.Name == "Weapon");
		if (weaponSlot == null) {
			Weapon.PlayNone();
		} else {
			Weapon.Play(weaponSlot.Animation);
		}

		var characterFrameTopLeftOffset = -characterFrame.Origin;
		Weapon.Position = (characterFrameTopLeftOffset + (weaponSlot?.Position ?? Vector2.Zero)) * new Vector2(Facing, 1);
		Weapon.Scale = new Vector2(Facing, 1);
	}

	private float GetJumpSpeed() {
		var time = Mathf.Sqrt(JumpHeight * 2 / Gravity);
		return time * Gravity;
	}

	public void MoveRight() => _moveDirection = Vector2.Right;
	public void MoveLeft() => _moveDirection = Vector2.Left;
	public void StopMoving() => _moveDirection = Vector2.Zero;
	public void MoveTowards(Vector2 target) => _moveDirection = (target - GlobalPosition).Normalized();

	public IEnumerable<Tiles.GameTileStairs> GetNearbyStairs() {
		return StairsDetection.GetOverlappingAreas().Where(a => a is Tiles.GameTileStairs).Cast<Tiles.GameTileStairs>();
	}

	public void Jump() => _jump = true;

	public void EnableWorldCollisions() => CollisionMask |= (uint)CollisionLayers.World;
	public void DisableWorldCollisions() => CollisionMask &= ~(uint)CollisionLayers.World;

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

	public bool IsStandingOnFloor() => FloorDetection.GetOverlappingBodies().Any();

	public void LoadCharacter(CharacterInfo character) {
		Character = character;
		Animation.SetCharacter(character);
		Weapon.SetAnimation(character.DefaultWeaponInfo?.Animations);
		CollisionShape.Position = new Vector2(0, -Character.Size.Y / 2);
		CollisionShape.Shape = new RectangleShape2D {
			Size = Character.Size
		};
		SetFloorShape();
	}

	public void LoadLevel(LevelInfo level) {
		Level = level;
		StairsShape.Shape = new RectangleShape2D {
			Size = new Vector2(level.TileSize.X * 3, level.TileSize.Y / 2f)
		};
		SetFloorShape();
	}

	private void SetFloorShape() {
		FloorShape.Shape = new RectangleShape2D {
			Size = new Vector2(Character?.Size.X ?? 0, Level?.TileSize.Y ?? 0 / 4f)
		};
	}
}