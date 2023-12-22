using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class TemporalNode : Node2D {
	public double StartTime { get; set; }
	public double EndTime { get; set; }
	public double CurrentTime { get; set; }
	public bool Active => StartTime <= CurrentTime && CurrentTime < EndTime;

	public override void _Process(double delta) {
		base._Process(delta);
		Visible = Active;
	}
}