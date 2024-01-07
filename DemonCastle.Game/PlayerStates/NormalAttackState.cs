namespace DemonCastle.Game.PlayerStates;

public class NormalAttackState : IState {
	public void OnEnter(GamePlayer player) {
		player.Animation.PlayStandAttack();
	}

	public IState? Update(GamePlayer player, double delta) {
		return player.Animation.IsComplete ? new NormalState() : null;
	}

	public void OnExit(GamePlayer player) {

	}
}