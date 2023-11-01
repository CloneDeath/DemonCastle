using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class TileView : TextureRect {
	public TileView(TileMapInfo tile) {
		Position = tile.Position.ToPixelPositionInArea();
		Texture = new AtlasTexture {
			Atlas = tile.Texture,
			Region = tile.Region,
			FilterClip = true
		};
		FlipH = tile.FlipHorizontal;
		Scale = tile.TileScale * tile.Span / tile.Region.Size ;
	}
}