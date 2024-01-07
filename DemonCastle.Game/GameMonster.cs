using System;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.EntityStates;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameMonster : CharacterBody2D, IDamageable, IEntityState {
	private readonly MonsterInfo _monster;
	private readonly MonsterDataInfo _monsterData;
	private readonly GameAnimation _animation;

	public Guid Id { get; } = Guid.NewGuid();

	public int Hp { get; set; }
	public int MaxHp => _monster.Health;
	public EntityStateMachine StateMachine { get; }

	public GameMonster(IGameState game, MonsterInfo monster, MonsterDataInfo monsterData, DebugState debug) {
		_monster = monster;
		_monsterData = monsterData;
		Name = nameof(GameMonster);

		AddChild(new CollisionShape2D {
			Position = new Vector2(0, -(float)Math.Floor(monster.Size.Y/2d)),
			Shape = new RectangleShape2D {
				Size = monster.Size
			},
			DebugColor = new Color(Colors.Green, 0.5f),
			Visible = debug.ShowCollisions
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;

		AddChild(_animation = new GameAnimation(this, debug));
		_animation.SetAnimation(monster.Animations);
		AddChild(new DebugPosition2D(debug));

		StateMachine = new EntityStateMachine(game, this, monster.InitialState, monster.States);

		Reset();
	}

	public void TakeDamage(int amount) {
		Hp -= amount;
		if (_monster.DespawnOnDeath && Hp <= 0) {
			_animation.PlayNone();
		}
	}

	public void Reset() {
		Position = _monsterData.MonsterPosition.ToPixelPositionInArea();
		Hp = MaxHp;

		StateMachine.Reset();
	}

	#region IEntityStatea
	public void SetFacing(int facing) {
		throw new NotImplementedException();
	}

	public Vector2 AreaPosition => Position;

	public void SetAnimation(Guid animationId) => _animation.Play(animationId);
	#endregion
}