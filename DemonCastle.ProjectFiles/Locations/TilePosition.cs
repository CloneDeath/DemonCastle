using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class TilePosition {
	private readonly Vector2I _tileIndex;
	private readonly AreaPosition _parentPosition;
	private readonly Vector2I _tileScaleInPixels;

	public TilePosition(Vector2I tileIndex, AreaPosition parentPosition, Vector2I tileScaleInPixels) {
		_tileIndex = tileIndex;
		_parentPosition = parentPosition;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2I ToTileIndex() => _tileIndex;
	public Vector2I ToPixelPositionInArea() => _tileIndex * _tileScaleInPixels;
	public Vector2I ToPixelPositionInLevel() => _tileIndex * _tileScaleInPixels + _parentPosition.ToPixelPositionInLevel();
}