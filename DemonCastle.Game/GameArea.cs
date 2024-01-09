using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.Tiles;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameArea : Node2D {
	private readonly IGameState _game;
	private readonly LevelInfo _level;
	private readonly AreaInfo _area;
	private readonly IGameLogger _logger;
	private readonly DebugState _debug;
	private StaticBody2D Body { get; }
	private List<GameMonster> Monsters { get; } = new();

	public AreaPosition AreaPosition => _area.PositionOfArea;

	private readonly List<Node> _spawned = new();

	public GameArea(IGameState game, ProjectInfo project, LevelInfo level, AreaInfo area, IGameLogger logger, DebugState debug) {
		_game = game;
		_level = level;
		_area = area;
		_logger = logger;
		_debug = debug;
		Name = nameof(GameArea);

		foreach (var tileMapInfo in area.TileMap) {
			var tileInfo = tileMapInfo.Tile;
			AddChild(new GameTile(tileInfo, debug) {
				Position = tileMapInfo.Position.ToPixelPositionInArea()
			});
		}

		foreach (var monsterData in area.Monsters) {
			var monster = project.Monsters.FirstOrDefault(m => m.Id == monsterData.MonsterId);
			if (monster == null) continue;
			var gameMonster = new GameMonster(game, level, monster, monsterData, logger, debug);
			Monsters.Add(gameMonster);
			AddChild(gameMonster);
		}
		AddChild(Body = new StaticBody2D {
			CollisionLayer = (uint)CollisionLayers.World
		});

		AddVoidBoundaries(area, level);
	}

	public void SpawnItem(ItemInfo item, Vector2 position) {
		var gameItem = new GameItem(_game, _level, item, _logger, _debug) {
			Position = position
		};
		AddChild(gameItem);
		_spawned.Add(gameItem);
	}

	private void AddVoidBoundaries(AreaInfo area, LevelInfo level) {
		var areaSize = area.SizeOfArea.ToAreaScale();
		for (var x = 0; x < areaSize.X; x++) {
			SetBoundary(area, level, new Vector2I(x, -1), new Vector2I(x, 0), new Vector2I(x + 1, 0));
			SetBoundary(area, level, new Vector2I(x, areaSize.Y), new Vector2I(x, areaSize.Y), new Vector2I(x+1, areaSize.Y));
		}
		for (var y = 0; y < areaSize.Y; y++) {
			SetBoundary(area, level, new Vector2I(-1, y), new Vector2I(0, y), new Vector2I(0, y + 1));
			SetBoundary(area, level, new Vector2I(areaSize.X, y), new Vector2I(areaSize.X, y), new Vector2I(areaSize.X, y+1));
		}
	}

	private void SetBoundary(AreaInfo area, LevelInfo level, Vector2I offsetToCheck, Vector2I start, Vector2I end) {
		var otherAreaPosition = area.PositionOfArea + offsetToCheck;
		var otherArea = level.GetAreaAt(otherAreaPosition);
		if (otherArea == null) {
			Body.AddChild(new CollisionShape2D {
				Shape = new SegmentShape2D {
					A = start * level.AreaScale.ToPixelSize(),
					B = end * level.AreaScale.ToPixelSize()
				},
				DebugColor = new Color(Colors.Black, 0.5f),
				Visible = _debug.ShowCollisions
			});
		}
	}

	public void OnPlayerEnter() {
		foreach (var monster in Monsters) {
			monster.Reset();
		}
		foreach (var entity in _spawned) {
			entity.QueueFree();
		}
	}
}