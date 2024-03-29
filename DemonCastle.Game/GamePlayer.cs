using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.Animations;
using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.PlayerStates;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GamePlayer : EntityCommon {
	protected LevelInfo? Level { get; set; }
	protected CharacterInfo? Character { get; set; }

	public CharacterAnimation Animation { get; }
	public GameAnimation Weapon { get; }
	public Area2D StairsDetection { get; }
	protected CollisionShape2D StairsShape { get; }
	public Area2D FloorDetection { get; }
	protected CollisionShape2D FloorShape { get; }

	private IFrameInfo? _previousFrame;

	public override float MoveSpeed => (Character?.WalkSpeed ?? 0) * (Level?.TileSize.X ?? 0);
	protected override float Gravity => (Character?.Gravity ?? 0) * (Level?.TileSize.Y ?? 0);
	public override float JumpHeight => (Character?.JumpHeight ?? 0) * (Level?.TileSize.Y ?? 0);

	private IState _state = new NormalState();

	public GamePlayer(IGameState game, IGameLogger logger, DebugState debug)
		: base(game, logger, debug) {
		Name = nameof(GamePlayer);

		State = new Variables(this);

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
	}

	public Variables State { get; }
	public Vector2 PositionInArea => GlobalPosition - (Game.CurrentArea?.Position.ToPixelPositionInLevel() ?? Vector2.Zero);

	protected override bool ApplyDamage(int amount) {
		State.Hp -= amount;
		if (State.Hp <= 0) {
			Visible = false;
		}
		return true;
	}

	public override void _EnterTree() {
		base._EnterTree();
		_state.OnEnter(this);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Character == null) return;

		AlignAnimationNodes();

		if (State.Hp <= 0) return;

		var nextState = _state.Update(this, delta);
		if (nextState != null) {
			_state.OnExit(this);
			_state = nextState;
			_state.OnEnter(this);
			Logger.StateChanged(_state);
		}

		if (GravityEnabled) {
			Velocity += new Vector2(0, (float)(Gravity * delta));
			Velocity = new Vector2(MoveDirection.X * MoveSpeed, MoveJump ? -GetJumpSpeed() : Velocity.Y);
		} else {
			Velocity = MoveDirection * MoveSpeed;
		}

		StopMoving();
		MoveJump = false;

		MoveAndSlide();

		UpdateWeaponFrame();
		_previousFrame = Animation.CurrentFrame ?? null;

		Animation.Scale = new Vector2(Facing, 1);
		AlignAnimationNodes();
	}

	private void UpdateWeaponFrame() {
		var characterFrame = Animation.CurrentFrame;
		if (characterFrame == null) return;
		if (characterFrame == _previousFrame) return;

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

	protected override void AlignAnimationNodes() {
		Animation.GlobalPosition = GlobalPosition.Round();
	}

	public void MoveTowards(Vector2 target) => MoveDirection = (target - GlobalPosition).Normalized();

	public IEnumerable<Tiles.GameTileStairs> GetNearbyStairs() {
		return StairsDetection.GetOverlappingAreas().Where(a => a is Tiles.GameTileStairs).Cast<Tiles.GameTileStairs>();
	}

	public void Jump() => MoveJump = true;

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
		Position = Level.StartingLocation;
	}

	private void SetFloorShape() {
		FloorShape.Shape = new RectangleShape2D {
			Size = new Vector2(Character?.Size.X ?? 0, (Level?.TileSize.Y ?? 0) / 4f)
		};
	}

	public void Reset() {
		Character = null;
		Level = null;
		Animation.Reset();
		Weapon.Reset();
	}
}