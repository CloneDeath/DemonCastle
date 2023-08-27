using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game; 

public partial class GameTile : Node2D {
	private readonly StaticBody2D Body;
	
	public GameTile(TileInfo tile, TileMapInfo tileMapInfo) {
		Position = tileMapInfo.Position;
		AddChild(new Sprite2D {
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false
		});
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