using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class AreaSize {
	private readonly Vector2I _areaSize;
	private readonly Vector2I _areaScaleInTiles;
	private readonly Vector2I _tileScaleInPixels;

	public AreaSize(Vector2I areaSize, Vector2I areaScaleInTiles, Vector2I tileScaleInPixels) {
		_areaSize = areaSize;
		_areaScaleInTiles = areaScaleInTiles;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2I ToPixelSize() {
		return _areaSize * _areaScaleInTiles * _tileScaleInPixels;
	}

	public Vector2I ToAreaScale() {
		return _areaSize;
	}
}