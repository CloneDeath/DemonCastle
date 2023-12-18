using System.Linq;
using DemonCastle.Game.Tiles;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameArea : Node2D {
	private StaticBody2D Body { get; }

	public GameArea(ProjectInfo project, LevelInfo level, AreaInfo area) {
		Name = nameof(GameArea);

		foreach (var tileMapInfo in area.TileMap) {
			var tileInfo = tileMapInfo.Tile;
			AddChild(new GameTile(tileInfo) {
				Position = tileMapInfo.Position.ToPixelPositionInArea()
			});
		}

		foreach (var monsterData in area.Monsters) {
			var monster = project.Monsters.FirstOrDefault(m => m.Id == monsterData.MonsterId);
			if (monster == null) continue;
			AddChild(new GameMonster(monster, monsterData));
		}
		AddChild(Body = new StaticBody2D {
			CollisionLayer = (uint)CollisionLayers.World
		});

		AddVoidBoundaries(area, level);
	}

	private void AddVoidBoundaries(AreaInfo area, LevelInfo level) {
		var areaSize = area.SizeOfArea.ToAreaScale();
		for (var x = 0; x < areaSize.X; x++) {
			SetBoundary(area, level, new Vector2I(x, -1), new Vector2I(x, 0), new Vector2I(x + 1, 0));
			SetBoundary(area, level, new Vector2I(x, areaSize.Y+1), new Vector2I(x, areaSize.Y), new Vector2I(x+1, areaSize.Y));
		}
		for (var y = 0; y < areaSize.Y; y++) {
			SetBoundary(area, level, new Vector2I(-1, y), new Vector2I(0, y), new Vector2I(0, y + 1));
			SetBoundary(area, level, new Vector2I(areaSize.X + 1, y), new Vector2I(areaSize.X, y), new Vector2I(areaSize.X, y+1));
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
				}
			});
		}
	}
}