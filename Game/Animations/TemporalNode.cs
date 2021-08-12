using Godot;

namespace DemonCastle.Game.Animations {
	public class TemporalNode : Node2D {
		public float StartTime { get; set; }
		public float EndTime { get; set; }
		public float CurrentTime { get; set; }

		public override void _Process(float delta) {
			base._Process(delta);

			Visible = StartTime <= CurrentTime && CurrentTime < EndTime;
		}
	}
}