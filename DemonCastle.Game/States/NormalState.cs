using System;
using System.Linq;
using DemonCastle.Game.Tiles;
using Godot;

namespace DemonCastle.Game.States;

public class NormalState : IState {
	public void OnEnter(GamePlayer player) {
		player.EnableGravity();
	}

	public IState? Update(GamePlayer player, double delta) {
		if (!player.IsStandingOnFloor()) {
			return new InAirState();
		}

		if (Input.IsActionJustPressed(InputActions.PlayerJump)) {
			player.Jump();
		}

		if (Input.IsActionJustPressed(InputActions.PlayerAttack)) {
			return new NormalAttackState();
		}

		var stairs = player.GetNearbyStairs().FirstOrDefault();
		var target = GetTargetInStairs(player, stairs);
		if (stairs != null && target != null) {
			if (target.PointsUp && Input.IsActionPressed(InputActions.PlayerMoveUp) ||
				!target.PointsUp && Input.IsActionPressed(InputActions.PlayerMoveDown)) {
				return new ApproachStairsState(stairs, target);
			}
		}

		if (Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			return new CrouchState();
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

	private static GameTileStairsNode? GetTargetInStairs(GamePlayer player, GameTileStairs? stairs) {
		if (stairs == null) return null;
		var startYDist = Math.Abs(stairs.Start.GlobalPosition.Y - player.GlobalPosition.Y);
		var endYDist = Math.Abs(stairs.End.GlobalPosition.Y - player.GlobalPosition.Y);
		return startYDist < endYDist ? stairs.Start : stairs.End;
	}

	public void OnExit(GamePlayer player) {
	}
}