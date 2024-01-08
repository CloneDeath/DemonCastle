using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public class PlayerVariables : IPlayerState {
	private readonly GamePlayer _player;

	public PlayerVariables(GamePlayer player) {
		_player = player;
	}

	public int HP { get; set; } = 10;
	public int MP { get; set; } = 8;
	public int Lives { get; set; } = 1;
	public int Score { get; set; } = 0;

	public Vector2 Position => _player.PositionInArea;
}