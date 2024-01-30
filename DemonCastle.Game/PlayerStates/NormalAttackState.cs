namespace DemonCastle.Game.PlayerStates;

public class NormalAttackState : IState {
	private float _duration;

	public void OnEnter(GamePlayer player) {
		player.Animation.PlayStandAttack();
		_duration = 0;
	}

	public IState? Update(GamePlayer player, double delta) {
		_duration += (float)delta;
		var animationComplete = player.Animation.IsComplete || _duration >= player.Animation.CurrentAnimationDuration;
		return animationComplete ? new NormalState() : null;
	}

	public void OnExit(GamePlayer player) {

	}
}