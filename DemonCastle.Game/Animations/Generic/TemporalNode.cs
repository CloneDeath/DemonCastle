using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class TemporalNode : Node2D {
	private double _currentTime;

	public double StartTime { get; set; }
	public double EndTime { get; set; }

	public double CurrentTime {
		get => _currentTime;
		set {
			_currentTime = value;
			Visible = Active;
			OnCurrentTimeChanged();
		}
	}

	public bool Active => StartTime <= CurrentTime && CurrentTime < EndTime;

	protected virtual void OnCurrentTimeChanged() {	}
}