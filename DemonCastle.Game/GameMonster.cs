using System;
using System.Linq;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using Godot;

namespace DemonCastle.Game;

public partial class GameMonster : CharacterBody2D, IDamageable {
	private readonly MonsterInfo _monster;
	private readonly MonsterDataInfo _monsterData;
	private readonly GameAnimation _animation;

	public Guid Id { get; } = Guid.NewGuid();

	public int Hp { get; set; }
	public int MaxHp => _monster.Health;

	public GameMonster(MonsterInfo monster, MonsterDataInfo monsterData, DebugState debug) {
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

		AddChild(_animation = new GameAnimation(monster.Animations, this, debug));
		AddChild(new DebugPosition2D(debug));

		Reset();
	}

	public void TakeDamage(int amount) {
		Hp -= amount;
		if (Hp <= 0) {
			_animation.PlayNone();
		}
	}

	public void Reset() {
		Position = _monsterData.MonsterPosition.ToPixelPositionInArea();
		Hp = MaxHp;

		var currentState = _monster.States.FirstOrDefault(m => m.Id == _monster.InitialState);
		_animation.Play(currentState?.Animation ?? Guid.Empty);
	}
}