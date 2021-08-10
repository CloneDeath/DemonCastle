using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer : KinematicBody2D {
		protected float WalkSpeed => Character.WalkSpeed * Level.TileWidth;
		public override void _Process(float delta) {
			base._Process(delta);

			var left = Input.IsActionPressed(InputActions.PlayerMoveLeft) ? 1 : 0;
			var right = Input.IsActionPressed(InputActions.PlayerMoveRight) ? 1 : 0;
			var deltaX = (right - left) * WalkSpeed;
			MoveAndSlide(new Vector2(deltaX, 0));
		}
	}
}