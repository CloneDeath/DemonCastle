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

	public GameMonster(MonsterInfo monster, MonsterDataInfo monsterData, DebugState debug) {
		_monster = monster;
		Name = nameof(GameMonster);
		Position = monsterData.MonsterPosition.ToPixelPositionInArea();
		Hp = MaxHp;

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

		GameAnimation animation;
		AddChild(animation = new GameAnimation(monster.Animations, this, debug));

		var currentState = monster.States.FirstOrDefault(m => m.Id == monster.InitialState);
		animation.Play(currentState?.Animation ?? Guid.Empty);

		AddChild(new DebugPosition2D(debug));
	}

	public Guid Id { get; } = Guid.NewGuid();

	public int Hp { get; set; }
	public int MaxHp => _monster.Health;

	public void TakeDamage(int amount) {
		Hp -= amount;
		if (Hp <= 0) {
			QueueFree();
		}
	}
}