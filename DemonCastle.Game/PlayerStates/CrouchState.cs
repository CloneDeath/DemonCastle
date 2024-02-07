using Godot;

namespace DemonCastle.Game.PlayerStates;

public class CrouchState : IState {
	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (!Input.IsActionPressed(InputActions.MoveDown)) {
			return new NormalState();
		}

		player.Animation.PlayCrouch();
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}