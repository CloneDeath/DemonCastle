using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTile : Node2D {
	private readonly TileInfo _tile;
	private StaticBody2D? Body { get; set; }
	private GameTileStairs? Stairs { get; set; }

	public GameTile(TileInfo tile, DebugState debug) {
		_tile = tile;

		Name = nameof(GameTile);

		AddChild(new Sprite2D {
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false,
			Scale = tile.Span * tile.TileSize / tile.Region.Size
		});

		SetupCollisions(debug);
		SetupStairs();
	}

	private void SetupCollisions(DebugState debug) {
		if (!_tile.Collision.Any()) return;
		AddChild(Body = new StaticBody2D {
			CollisionLayer = (uint)CollisionLayers.World
		});
		Body.AddChild(new CollisionShape2D {
			Shape = new ConvexPolygonShape2D {
				Points = _tile.Collision.Select(v => v * _tile.TileSize * _tile.Span).ToArray()
			},
			DebugColor = new Color(Colors.Aqua, 0.5f),
			Visible = debug.ShowCollisions
		});
	}

	private void SetupStairs() {
		if (_tile.Stairs == null) return;

		AddChild(Stairs = new GameTileStairs(_tile, _tile.Stairs));
	}
}