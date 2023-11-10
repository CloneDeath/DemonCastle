using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameTileStairs : Area2D {
	public Node2D Start { get; }
	public Node2D End { get; }

	public GameTileStairs(TileInfo tile, StairData tileStairs) {
		Name = nameof(GameTileStairs);
		CollisionLayer = (uint)CollisionLayers.World;
		CollisionMask = (uint)CollisionLayers.Player;

		var size = tile.TileSize * tile.Span;
		AddChild(new CollisionShape2D {
			Position = size/2,
			Shape = new RectangleShape2D {
				Size = size
			}
		});

		AddChild(Start = new Node2D {
			Position = new Vector2(tileStairs.Start.X, tileStairs.Start.Y) * tile.TileSize * tile.Span
		});
		AddChild(End = new Node2D {
			Position = new Vector2(tileStairs.End.X, tileStairs.End.Y) * tile.TileSize * tile.Span
		});
	}
}