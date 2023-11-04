using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class AreaRegion {
	private readonly AreaPosition _position;
	private readonly AreaSize _size;

	public AreaRegion(AreaPosition position, AreaSize size) {
		_position = position;
		_size = size;
	}

	public bool HasPixelPositionInLevel(Vector2I pixelPosition) {
		var rect = ToPixelRegionInLevel();
		return rect.HasPoint(pixelPosition);
	}

	public bool ContainsAreaIndex(Vector2I areaIndex) {
		var rect = new Rect2I(_position.AreaIndex, _size.ToAreaScale());
		return rect.HasPoint(areaIndex);
	}

	public Rect2I ToPixelRegionInLevel() {
		return new Rect2I(_position.ToPixelPositionInLevel(), _size.ToPixelSize());
	}
}