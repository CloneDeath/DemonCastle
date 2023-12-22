using System;
using System.Linq;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using Godot;

namespace DemonCastle.Game;

public partial class GameMonster : CharacterBody2D {
	public GameMonster(MonsterInfo monster, MonsterDataInfo monsterData, DebugState debug) {
		Name = nameof(GameMonster);
		Position = monsterData.MonsterPosition.ToPixelPositionInArea();

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
		AddChild(animation = new GameAnimation(monster.Animations, debug));

		var currentState = monster.States.FirstOrDefault(m => m.Id == monster.InitialState);
		animation.Play(currentState?.Animation ?? Guid.Empty);

		AddChild(new DebugPosition2D(debug));
	}
}