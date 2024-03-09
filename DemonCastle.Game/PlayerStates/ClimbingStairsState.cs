using System.Linq;
using Godot;

namespace DemonCastle.Game.PlayerStates;

public class ClimbingStairsState : IState {
	private readonly Tiles.GameTileStairs _stairs;

	public ClimbingStairsState(Tiles.GameTileStairs stairs) {
		_stairs = stairs;
	}

	public void OnEnter(GamePlayer player) {
		player.DisableGravity();
		player.DisableWorldCollisions();
	}

	public IState? Update(GamePlayer player, double delta) {
		return Input.IsActionPressed(InputActions.MoveUp) ? MoveTowards(player, UpTarget, player.GlobalPosition.Y < UpTarget.GlobalPosition.Y)
			   : Input.IsActionPressed(InputActions.MoveDown) ? MoveTowards(player, DownTarget, player.GlobalPosition.Y > DownTarget.GlobalPosition.Y)
			   : Input.IsActionPressed(InputActions.MoveLeft) ? MoveTowards(player, LeftTarget, player.GlobalPosition.X < LeftTarget.GlobalPosition.X)
			   : Input.IsActionPressed(InputActions.MoveRight) ? MoveTowards(player, RightTarget, player.GlobalPosition.X > RightTarget.GlobalPosition.X)
			   : null;
	}

	private IState? MoveTowards(GamePlayer player, Node2D target, bool isBeyondTarget) {
		if (!isBeyondTarget) {
			player.MoveTowards(target.GlobalPosition);
			if (player.GlobalPosition.DistanceTo(target.GlobalPosition) > 0.5) return null;
		}

		var stairs = player.GetNearbyStairs().FirstOrDefault(s => s != _stairs);
		return stairs == null ? new NormalState() : new ClimbingStairsState(stairs);
	}

	private Node2D UpTarget => _stairs.Start.Position.Y < _stairs.End.Position.Y ? _stairs.Start : _stairs.End;
	private Node2D DownTarget => _stairs.Start.Position.Y < _stairs.End.Position.Y ? _stairs.End : _stairs.Start;
	private Node2D LeftTarget => _stairs.Start.Position.X < _stairs.End.Position.X ? _stairs.Start : _stairs.End;
	private Node2D RightTarget => _stairs.Start.Position.X < _stairs.End.Position.X ? _stairs.End : _stairs.Start;

	public void OnExit(GamePlayer player) {
		player.EnableWorldCollisions();
	}
}