using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameLevel : Node2D {
	private readonly ProjectInfo _project;
	private readonly DebugState _debug;

	protected LevelInfo? Level { get; set; }
	protected readonly Dictionary<AreaInfo, GameArea> _areaMap = new();

	public Vector2 StartingLocation => Level?.StartingLocation ?? Vector2.Zero;

	public GameLevel(ProjectInfo project, DebugState debug) {
		_project = project;
		_debug = debug;
		Name = nameof(GameLevel);
	}

	public AreaInfo? GetAreaAtPoint(Vector2I point) {
		return Level?.Areas.FirstOrDefault(area => area.Region.HasPixelPositionInLevel(point));
	}

	public GameArea? GetGameAreaAtPoint(Vector2I point) {
		var areaInfo = GetAreaAtPoint(point);
		return areaInfo == null ? null : _areaMap[areaInfo];
	}

	public void LoadLevel(LevelInfo level) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		Level = level;
		_areaMap.Clear();

		foreach (var area in Level.Areas) {
			var gameArea = new GameArea(_project, level, area, _debug) {
				Position = area.PositionOfArea.ToPixelPositionInLevel()
			};
			_areaMap[area] = gameArea;
			AddChild(gameArea);
		}
	}
}