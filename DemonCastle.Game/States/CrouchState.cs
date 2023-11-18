using Godot;

namespace DemonCastle.Game.States;

public class CrouchState : IState {
	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (!Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			return new NormalState();
		}

		player.Animation.PlayCrouch();
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}