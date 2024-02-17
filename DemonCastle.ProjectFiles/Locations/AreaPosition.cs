using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public partial class AreaPosition {
	public Vector2I AreaIndex { get; }
	private readonly Vector2I _areaScaleInTiles;
	private readonly Vector2I _tileScaleInPixels;

	public AreaPosition(Vector2I areaIndex, Vector2I areaScaleInTiles, Vector2I tileScaleInPixels) {
		AreaIndex = areaIndex;
		_areaScaleInTiles = areaScaleInTiles;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2I ToPixelPositionInLevel() {
		return AreaIndex * _areaScaleInTiles * _tileScaleInPixels;
	}

	public override string ToString() => AreaIndex.ToString();
}