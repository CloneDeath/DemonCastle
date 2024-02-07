using Godot;

namespace DemonCastle.Game.PlayerStates;

public class InAirState : IState {
	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (player.IsStandingOnFloor()) {
			return new NormalState();
		}

		player.Animation.PlayJump();

		var left = Input.IsActionPressed(InputActions.MoveLeft) ? 1 : 0;
		var right = Input.IsActionPressed(InputActions.MoveRight) ? 1 : 0;
		if (right - left == 0) {
			player.StopMoving();
		}
		else {
			if (right > left) player.MoveRight();
			else player.MoveLeft();
		}
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}