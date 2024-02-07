using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameLevel : Node2D {
	private readonly ProjectResources _resources;
	private readonly IGameState _game;
	private readonly IGameLogger _logger;
	private readonly DebugState _debug;

	protected LevelInfo? Level { get; set; }
	protected readonly Dictionary<AreaInfo, GameArea> AreaMap = new();

	public Vector2 StartingLocation => Level?.StartingLocation ?? Vector2.Zero;

	public GameLevel(ProjectResources resources, IGameState game, IGameLogger logger, DebugState debug) {
		_resources = resources;
		_game = game;
		_logger = logger;
		_debug = debug;
		Name = nameof(GameLevel);
	}

	public AreaInfo? GetAreaAtPoint(Vector2I point) {
		return Level?.Areas.FirstOrDefault(area => area.Region.HasPixelPositionInLevel(point));
	}

	public GameArea? GetGameAreaAtPoint(Vector2I point) {
		var areaInfo = GetAreaAtPoint(point);
		return areaInfo == null ? null : AreaMap[areaInfo];
	}

	public void LoadLevel(LevelInfo level) {
		ClearLevel();
		Level = level;

		foreach (var area in Level.Areas) {
			var gameArea = new GameArea(_resources, _game, level, area, _logger, _debug) {
				Position = area.PositionOfArea.ToPixelPositionInLevel()
			};
			AreaMap[area] = gameArea;
			AddChild(gameArea);
		}
	}

	public void Reset() {
		ClearLevel();
		Level = null;
	}

	private void ClearLevel() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		AreaMap.Clear();
	}
}