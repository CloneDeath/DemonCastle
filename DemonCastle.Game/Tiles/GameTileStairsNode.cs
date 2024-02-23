using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTileStairsNode : Node2D {
	private readonly IStairInfo _stairs;
	private readonly bool _start;

	public GameTileStairsNode(IStairInfo stairs, bool start) {
		_stairs = stairs;
		_start = start;
	}

	private Vector2 Self => _start ? _stairs.Start : _stairs.End;
	private Vector2 Other => _start ? _stairs.End : _stairs.Start;
	public bool PointsUp => Self.Y > Other.Y;
	public int Facing => Self.X < Other.X ? 1 : -1;
}