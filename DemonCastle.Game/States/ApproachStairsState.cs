using System.Linq;
using Godot;

namespace DemonCastle.Game.States;

public class ApproachStairsState : IState {
	private readonly Tiles.GameTileStairs _stairs;
	private readonly Vector2 _target;
	private readonly bool _upStairs;

	public ApproachStairsState(Tiles.GameTileStairs stairs, Vector2 target, bool upStairs) {
		_stairs = stairs;
		_target = target;
		_upStairs = upStairs;
	}

	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (_upStairs && !Input.IsActionPressed(InputActions.PlayerMoveUp)) {
			return new NormalState();
		}
		if (!_upStairs && !Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			return new NormalState();
		}

		player.Facing = _target.X > player.GlobalPosition.X ? 1 : -1;
		player.Animation.PlayWalk();

		var distanceToTarget = player.GlobalPosition.DistanceTo(_target);
		if (distanceToTarget <= player.WalkSpeed * delta) {
			return new ClimbingStairsState(_stairs);
		}

		if (!player.GetNearbyStairs().Any()) {
			return new NormalState();
		}

		player.MoveTowards(_target);
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}