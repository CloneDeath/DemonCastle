using System.Linq;
using DemonCastle.Game.Tiles;
using Godot;

namespace DemonCastle.Game.States;

public class ApproachStairsState : IState {
	private readonly GameTileStairs _stairs;
	private readonly GameTileStairsNode _target;

	public ApproachStairsState(GameTileStairs stairs, GameTileStairsNode target) {
		_stairs = stairs;
		_target = target;
	}

	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (_target.PointsUp && !Input.IsActionPressed(InputActions.PlayerMoveUp)) {
			return new NormalState();
		}
		if (!_target.PointsUp && !Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			return new NormalState();
		}

		player.Facing = _target.GlobalPosition.X > player.GlobalPosition.X ? 1 : -1;
		player.Animation.PlayWalk();

		var distanceToTarget = player.GlobalPosition.DistanceTo(_target.GlobalPosition);
		if (distanceToTarget <= player.WalkSpeed * delta) {
			player.Facing = _target.Facing;

			if (_target.PointsUp) {
				player.Animation.PlayStairsUp();
			}
			else {
				player.Animation.PlayStairsDown();
			}

			return new ClimbingStairsState(_stairs);
		}

		if (!player.GetNearbyStairs().Any()) {
			return new NormalState();
		}

		player.MoveTowards(_target.GlobalPosition);
		return null;
	}

	public void OnExit(GamePlayer player) {
	}
}