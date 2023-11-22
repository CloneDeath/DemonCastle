using Godot;

namespace DemonCastle.Game.Animations.Generic; 

public partial class TemporalNode : Node2D {
	public double StartTime { get; set; }
	public double EndTime { get; set; }
	public double CurrentTime { get; set; }

	public override void _Process(double delta) {
		base._Process(delta);

		Visible = StartTime <= CurrentTime && CurrentTime < EndTime;
	}
}