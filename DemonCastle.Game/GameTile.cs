using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameTile : Node2D {
	private StaticBody2D? Body { get; }

	public GameTile(TileInfo tile) {
		AddChild(new Sprite2D {
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false,
			Scale = tile.Span * tile.TileSize / tile.Region.Size
		});

		if (!tile.Collision.Any()) return;
		AddChild(Body = new StaticBody2D {
			CollisionLayer = (uint)CollisionLayers.World
		});
		Body.AddChild(new CollisionShape2D {
			Shape = new ConvexPolygonShape2D {
				Points = tile.Collision
			}
		});
	}
}