namespace DemonCastle.Game.PlayerStates;

public interface IState {
	void OnEnter(GamePlayer player);
	IState? Update(GamePlayer player, double delta);
	void OnExit(GamePlayer player);
}