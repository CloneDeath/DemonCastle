using Godot;

namespace DemonCastle.Game.States;

public class ClimbingStairsState : IState {
	public ClimbingStairsState(GameTileStairs stairs) {

	}

	public void OnEnter(GamePlayer player) {
		player.ApplyGravity = false;
		player.StopMoving();
		GD.Print("State: ClimbingStairsState");
	}

	public IState? Update(GamePlayer player, double delta) {
		return null;
	}

	public void OnExit(GamePlayer player) {

	}
}