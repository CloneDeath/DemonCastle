using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameLevel : Node2D {
	protected LevelInfo Level { get; }
	protected Dictionary<AreaInfo, GameArea> _areaMap = new();

	public Vector2 StartingLocation => Level.StartingLocation;

	public GameLevel(ProjectInfo project, LevelInfo level, DebugState debug) {
		Level = level;

		Name = nameof(GameLevel);

		foreach (var area in Level.Areas) {
			var gameArea = new GameArea(project, level, area, debug) {
				Position = area.PositionOfArea.ToPixelPositionInLevel()
			};
			_areaMap[area] = gameArea;
			AddChild(gameArea);
		}
	}

	public AreaInfo? GetAreaAtPoint(Vector2I point) {
		return Level.Areas.FirstOrDefault(area => area.Region.HasPixelPositionInLevel(point));
	}

	public GameArea? GetGameAreaAtPoint(Vector2I point) {
		var areaInfo = GetAreaAtPoint(point);
		return areaInfo == null ? null : _areaMap[areaInfo];
	}
}