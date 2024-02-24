using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTileStairs : Area2D {
	public GameTileStairsNode Start { get; }
	public GameTileStairsNode End { get; }

	public GameTileStairs(LevelInfo level, ITileInfo tile, IStairInfo tileStairs, DebugState debug) {
		Name = nameof(GameTileStairs);

		CollisionLayer = (uint)CollisionLayers.World;
		CollisionMask = (uint)CollisionLayers.Player;
		ProcessMode = ProcessModeEnum.Always;

		var size = level.TileSize * tile.Size;
		AddChild(new CollisionShape2D {
			Position = size/2,
			Shape = new RectangleShape2D {
				Size = size
			},
			DebugColor = new Color(Colors.Purple, 0.5f),
			Visible = debug.ShowCollisions
		});

		AddChild(Start = new GameTileStairsNode(tileStairs, true) {
			Position = new Vector2(tileStairs.Start.X, tileStairs.Start.Y) * level.TileSize * tile.Size
		});
		AddChild(End = new GameTileStairsNode(tileStairs, false) {
			Position = new Vector2(tileStairs.End.X, tileStairs.End.Y) * level.TileSize * tile.Size
		});
	}
}