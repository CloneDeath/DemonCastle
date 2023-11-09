using Godot;

namespace DemonCastle.Game.States;

public class NormalState : IState {
	public void OnEnter(GamePlayer player) {
		player.ApplyGravity = true;
	}

	public IState? Update(GamePlayer player, double delta) {
		if (Input.IsActionJustPressed(InputActions.PlayerJump)) {
			player.Jump();
		}

		var left = Input.IsActionPressed(InputActions.PlayerMoveLeft) ? 1 : 0;
		var right = Input.IsActionPressed(InputActions.PlayerMoveRight) ? 1 : 0;
		if (right - left == 0) {
			player.StopMoving();
			player.Animation.PlayIdle();
		}
		else {
			if (right > left) player.MoveRight();
			else player.MoveLeft();
			player.Animation.PlayWalk();
			player.Facing = right - left;
		}
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}