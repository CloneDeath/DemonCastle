using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class TileSize {
	private readonly Vector2I _areaSizeInTiles;
	private readonly Vector2I _tileSizeInPixels;

	public TileSize(Vector2I areaSizeInTiles, Vector2I tileSizeInPixels) {
		_areaSizeInTiles = areaSizeInTiles;
		_tileSizeInPixels = tileSizeInPixels;
	}

	public Vector2I ToPixelSize() {
		return _areaSizeInTiles * _tileSizeInPixels;
	}
}