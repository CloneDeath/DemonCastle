using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameLevel : Node2D {
	protected LevelInfo Level { get; }

	public Vector2 StartingLocation => Level.StartingLocation;

	public GameLevel(ProjectInfo project, LevelInfo level, DebugState debug) {
		Level = level;

		Name = nameof(GameLevel);

		foreach (var area in Level.Areas) {
			AddChild(new GameArea(project, level, area, debug) {
				Position = area.PositionOfArea.ToPixelPositionInLevel()
			});
		}
	}

	public AreaInfo? GetAreaAtPoint(Vector2I point) {
		return Level.Areas.FirstOrDefault(area => area.Region.HasPixelPositionInLevel(point));
	}
}