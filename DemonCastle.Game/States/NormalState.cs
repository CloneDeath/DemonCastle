using System;
using System.Linq;
using Godot;

namespace DemonCastle.Game.States;

public class NormalState : IState {
	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		var stairs = player.GetNearbyStairs().FirstOrDefault();
		var target = GetTargetInStairs(player, stairs);
		if (stairs != null && target != null) {
			var isUp = GetStairDirection(stairs, target.Value);
			if (isUp && Input.IsActionPressed(InputActions.PlayerMoveUp) ||
				!isUp && Input.IsActionPressed(InputActions.PlayerMoveDown)) {
				return new ApproachStairsState(stairs, target.Value, isUp);
			}
		}
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

	private static bool GetStairDirection(GameTileStairs stairs, Vector2 target) {
		var startDist = stairs.Start.GlobalPosition.DistanceTo(target);
		var endDist = stairs.End.GlobalPosition.DistanceTo(target);
		var from = startDist < endDist ? stairs.Start.GlobalPosition : stairs.End.GlobalPosition;
		var to = startDist < endDist ? stairs.End.GlobalPosition : stairs.Start.GlobalPosition;
		return from.Y > to.Y;
	}

	private static Vector2? GetTargetInStairs(GamePlayer player, GameTileStairs? stairs) {
		if (stairs == null) return null;
		var startYDist = Math.Abs(stairs.Start.GlobalPosition.Y - player.GlobalPosition.Y);
		var endYDist = Math.Abs(stairs.End.GlobalPosition.Y - player.GlobalPosition.Y);
		return startYDist < endYDist ? stairs.Start.GlobalPosition : stairs.End.GlobalPosition;
	}

	public void OnExit(GamePlayer player) {
	}
}