using Godot;

namespace DemonCastle.Game.States;

public class ApproachStairsState : IState {
	private readonly GameTileStairs _stairs;
	private readonly Vector2 _target;
	private readonly bool _upStairs;

	public ApproachStairsState(GameTileStairs stairs, Vector2 target, bool upStairs) {
		_stairs = stairs;
		_target = target;
		_upStairs = upStairs;
	}

	public void OnEnter(GamePlayer player) {

	}

	public IState? Update(GamePlayer player, double delta) {
		if (_upStairs && !Input.IsActionPressed(InputActions.PlayerMoveUp)) {
			return new NormalState();
		}
		if (!_upStairs && !Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			return new NormalState();
		}

		var distanceToTarget = player.GlobalPosition.DistanceTo(_target);
		if (distanceToTarget <= player.WalkSpeed * delta) {
			return new ClimbingStairsState(_stairs);
		}

		player.MoveTo(_target);
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}