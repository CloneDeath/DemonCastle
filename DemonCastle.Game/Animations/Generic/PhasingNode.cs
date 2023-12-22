using System;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class PhasingNode : Node2D {
	private List<TemporalNode> Nodes { get; } = new();

	public event Action<PhasingNode>? Complete;

	public double Duration { get; set; } = 1;
	public double CurrentTime { get; set; }
	public bool Playing { get; set; }

	public override void _Process(double delta) {
		base._Process(delta);

		if (!Playing) return;

		CurrentTime += delta;
		if (CurrentTime > Duration) {
			Complete?.Invoke(this);
			CurrentTime %= Duration;
		}
		foreach (var temporalNode in Nodes) {
			temporalNode.CurrentTime = CurrentTime;
		}
	}

	public void AddPhase(Node node, float startTime, float endTime) {
		var child = new TemporalNode {
			StartTime = startTime,
			EndTime = endTime
		};
		child.AddChild(node);
		AddPhase(child);
	}

	public void AddPhase(TemporalNode child) {
		Nodes.Add(child);
		AddChild(child);
	}

	public void Play() {
		Playing = true;
	}
}