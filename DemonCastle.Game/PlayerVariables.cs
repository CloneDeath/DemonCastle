using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game;

public class PlayerVariables : IPlayerState {
	public int HP { get; set; } = 10;
	public int MP { get; set; } = 8;
	public int Lives { get; set; } = 1;
	public int Score { get; set; } = 0;
}