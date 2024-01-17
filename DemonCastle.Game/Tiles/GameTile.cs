using System.Linq;
using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTile : GameBaseEntity {
	private readonly TileInfo _tile;
	private StaticBody2D? Body { get; set; }
	private GameTileStairs? Stairs { get; set; }

	public GameTile(IGameState game, LevelInfo level, TileInfo tile, IGameLogger logger, DebugState debug)
		: base(game, level, tile, logger,debug) {
		_tile = tile;

		Name = nameof(GameTile);

		AddChild(new Sprite2D {
			Name = "TileTexture",
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false,
			Scale = tile.Size * tile.TileSize / tile.Region.Size
		});

		SetupCollisions(debug);
		SetupStairs(debug);
	}

	private void SetupCollisions(DebugState debug) {
		if (!_tile.Collision.Any()) return;
		AddChild(Body = new StaticBody2D {
			Name = "CollisionBody",
			CollisionLayer = (uint)CollisionLayers.World
		});
		Body.AddChild(new CollisionShape2D {
			Shape = new ConvexPolygonShape2D {
				Points = _tile.Collision.Select(v => v * _tile.TileSize * _tile.Size).ToArray()
			},
			DebugColor = new Color(Colors.Aqua, 0.5f),
			Visible = debug.ShowCollisions
		});
	}

	private void SetupStairs(DebugState debug) {
		if (_tile.Stairs == null) return;

		AddChild(Stairs = new GameTileStairs(_tile, _tile.Stairs, debug));
	}

	public override float MoveSpeed => 0;
	protected override float Gravity => 0;
	protected override bool ApplyDamage(int amount) => false;
	protected override bool IsImmobile => true;
	public override bool WasKilled => false;
}