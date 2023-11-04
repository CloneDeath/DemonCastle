using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class AreaRegion {
	private readonly AreaPosition _position;
	private readonly AreaSize _size;

	public AreaRegion(AreaPosition position, AreaSize size) {
		_position = position;
		_size = size;
	}

	public bool HasPixelPositionInLevel(Vector2 pixelPosition) {
		var rect = new Rect2(_position.ToPixelPositionInLevel(), _size.ToPixelSize());
		return rect.HasPoint(pixelPosition);
	}
}