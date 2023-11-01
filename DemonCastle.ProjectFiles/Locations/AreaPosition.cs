using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class AreaPosition {
	private readonly Vector2I _areaIndex;
	private readonly Vector2I _areaScaleInTiles;
	private readonly Vector2I _tileScaleInPixels;

	public AreaPosition(Vector2I areaIndex, Vector2I areaScaleInTiles, Vector2I tileScaleInPixels) {
		_areaIndex = areaIndex;
		_areaScaleInTiles = areaScaleInTiles;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2I ToPixelPositionInLevel() {
		return _areaIndex * _areaScaleInTiles * _tileScaleInPixels;
	}
}