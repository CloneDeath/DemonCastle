using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileCell : Node2D {
	public TileCell(TileMapInfo tile) {
		Position = tile.AreaPosition;

		AddChild(new Sprite2D {
			Texture = tile.Texture,
			RegionEnabled = true,
			RegionRect = tile.Region,
			Centered = false
		});
	}
}