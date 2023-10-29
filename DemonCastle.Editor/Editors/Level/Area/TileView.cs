using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class TileView : Sprite2D {
	public TileView(TileMapInfo tile) {
		Position = tile.AreaPosition;
		Texture = tile.Texture;
		RegionEnabled = true;
		RegionRect = tile.Region;
		Centered = false;
		Scale = tile.TileSize / tile.Region.Size;
	}
}