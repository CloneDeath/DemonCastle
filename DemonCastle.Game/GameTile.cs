using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game; 

public partial class GameTile : Node2D {
	public GameTile(TileInfo tile, TileMapInfo tileMapInfo) {
		Position = tileMapInfo.Position;
		AddChild(new Sprite2D {
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false
		});
	}
}