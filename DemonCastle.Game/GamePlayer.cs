using Godot;

namespace DemonCastle.Game {
	public partial class GamePlayer : CharacterBody2D {
		protected float WalkSpeed => Character.WalkSpeed * Level.TileSize.X;
		protected float Gravity => Character.Gravity * Level.TileSize.Y;
		protected float JumpHeight => Character.JumpHeight * Level.TileSize.Y;
		protected int Facing { get; set; } = 1;
		
		public override void _Process(double delta) {
			base._Process(delta);

			var left = Input.IsActionPressed(InputActions.PlayerMoveLeft) ? 1 : 0;
			var right = Input.IsActionPressed(InputActions.PlayerMoveRight) ? 1 : 0;
			var deltaX = (right - left) * WalkSpeed;
			if (Input.IsActionJustPressed(InputActions.PlayerJump)) {
				Velocity = -new Vector2(Velocity.X, GetJumpSpeed());
			}

			Velocity = new Vector2(deltaX, (float)(Velocity.Y + Gravity * delta));
			MoveAndSlide();

			if (right - left == 0) {
				Animation.PlayIdle();
			}
			else {
				Animation.PlayWalk();
				Facing = right - left;
			}

			Animation.Scale = new Vector2(Facing, 1);
		}

		private float GetJumpSpeed() {
			var time = Mathf.Sqrt(JumpHeight * 2 / Gravity);
			return time * Gravity;
		}
	}
}