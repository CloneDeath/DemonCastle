using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class AreaPosition {
	private readonly Vector2I _levelPosition;
	private readonly Vector2I _areaScaleInTiles;
	private readonly Vector2I _tileScaleInPixels;

	public AreaPosition(Vector2I levelPosition, Vector2I areaScaleInTiles, Vector2I tileScaleInPixels) {
		_levelPosition = levelPosition;
		_areaScaleInTiles = areaScaleInTiles;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2I ToLevelPositionInPixels() {
		return _levelPosition * _areaScaleInTiles * _tileScaleInPixels;
	}
}