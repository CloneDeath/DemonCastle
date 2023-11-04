using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameLevel : Node2D {
	protected LevelInfo Level { get; }

	public Vector2 StartingLocation => Level.StartingLocation;

	public GameLevel(LevelInfo level) {
		Level = level;
		LoadLevel();
	}

	protected void LoadLevel() {
		foreach (var area in Level.Areas) {
			LoadArea(area);
		}
	}

	protected void LoadArea(AreaInfo area) {
		foreach (var tileMapInfo in area.TileMap) {
			var tileInfo = tileMapInfo.Tile;
			AddChild(new GameTile(tileInfo, tileMapInfo));
		}
	}

	public AreaInfo? GetAreaAtPoint(Vector2 point) {
		return Level.Areas.FirstOrDefault(area => area.Region.HasPixelPositionInLevel(point));
	}
}