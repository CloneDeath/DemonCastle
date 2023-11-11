using Godot;

namespace DemonCastle.Game.States;

public class ClimbingStairsState : IState {
	private readonly GameTileStairs _stairs;

	public ClimbingStairsState(GameTileStairs stairs) {
		_stairs = stairs;
	}

	public void OnEnter(GamePlayer player) {
		player.ApplyGravity = false;
	}

	public IState? Update(GamePlayer player, double delta) {
		if (Input.IsActionPressed(InputActions.PlayerMoveUp)) {
			player.MoveTowards(UpTarget.GlobalPosition);
		}
		if (Input.IsActionPressed(InputActions.PlayerMoveDown)) {
			player.MoveTowards(DownTarget.GlobalPosition);
		}
		if (Input.IsActionPressed(InputActions.PlayerMoveLeft)) {
			player.MoveTowards(LeftTarget.GlobalPosition);
		}
		if (Input.IsActionPressed(InputActions.PlayerMoveRight)) {
			player.MoveTowards(RightTarget.GlobalPosition);
		}
		return null;
	}

	private Node2D UpTarget => _stairs.Start.Position.Y < _stairs.End.Position.Y ? _stairs.Start : _stairs.End;
	private Node2D DownTarget => _stairs.Start.Position.Y < _stairs.End.Position.Y ? _stairs.End : _stairs.Start;
	private Node2D LeftTarget => _stairs.Start.Position.X < _stairs.End.Position.X ? _stairs.Start : _stairs.End;
	private Node2D RightTarget => _stairs.Start.Position.X < _stairs.End.Position.X ? _stairs.End : _stairs.Start;

	public void OnExit(GamePlayer player) {

	}
}