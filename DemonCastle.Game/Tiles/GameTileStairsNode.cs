using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTileStairsNode : Node2D {
	private readonly StairData _stairs;
	private readonly bool _start;

	public GameTileStairsNode(StairData stairs, bool start) {
		_stairs = stairs;
		_start = start;
	}

	private Position2D Self => _start ? _stairs.Start : _stairs.End;
	private Position2D Other => _start ? _stairs.End : _stairs.Start;
}