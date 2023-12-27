using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class MonsterPosition {
	private readonly Vector2 _position;
	private readonly AreaPosition _parentPosition;
	private readonly Vector2I _tileScaleInPixels;

	public MonsterPosition(Vector2 position, AreaPosition parentPosition, Vector2I tileScaleInPixels) {
		_position = position;
		_parentPosition = parentPosition;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Vector2 ToPixelPositionInArea() => _position * _tileScaleInPixels;
	public Vector2 ToPixelPositionInLevel() => _position * _tileScaleInPixels + _parentPosition.ToPixelPositionInLevel();
}