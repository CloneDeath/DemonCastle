using DemonCastle.ProjectFiles.Extensions;
using DemonCastle.ProjectFiles.Files;
using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public class MonsterPosition {
	private readonly Position2D _position;
	private readonly AreaPosition _parentPosition;
	private readonly Vector2I _tileScaleInPixels;

	public MonsterPosition(Position2D position, AreaPosition parentPosition, Vector2I tileScaleInPixels) {
		_position = position;
		_parentPosition = parentPosition;
		_tileScaleInPixels = tileScaleInPixels;
	}

	public Position2D ToPosition2D() => _position;
	public Vector2 ToPixelPositionInArea() => _position.ToVector2() * _tileScaleInPixels;
	public Vector2 ToPixelPositionInLevel() => _position.ToVector2() * _tileScaleInPixels + _parentPosition.ToPixelPositionInLevel();
}