using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer : KinematicBody2D {
		protected float WalkSpeed => Character.WalkSpeed * Level.TileWidth;
		protected int Facing { get; set; } = 1;
		
		protected float VSpeed { get; set; }
		
		public override void _Process(float delta) {
			base._Process(delta);

			var left = Input.IsActionPressed(InputActions.PlayerMoveLeft) ? 1 : 0;
			var right = Input.IsActionPressed(InputActions.PlayerMoveRight) ? 1 : 0;
			var deltaX = (right - left) * WalkSpeed;
			if (Input.IsActionJustPressed(InputActions.PlayerJump)) {
				VSpeed = -96;
			}

			VSpeed += 32 * delta;
			var actual = MoveAndSlide(new Vector2(deltaX, VSpeed));
			VSpeed = actual.y;

			if (right - left == 0) {
				Animation.PlayIdle();
			}
			else {
				Animation.PlayWalk();
				Facing = right - left;
			}

			Animation.Scale = new Vector2(Facing, 1);
		}
	}
}