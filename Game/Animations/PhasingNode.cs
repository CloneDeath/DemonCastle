using System.Collections.Generic;
using Godot;

namespace DemonCastle.Game.Animations {
	public class PhasingNode : Node2D {
		private List<TemporalNode> Nodes { get; } = new List<TemporalNode>();

		public float Duration { get; set; } = 1;
		public float CurrentTime { get; set; } = 0;
		public bool Playing { get; set; } = false;

		public override void _Process(float delta) {
			base._Process(delta);

			if (!Playing) return;

			CurrentTime = (CurrentTime + delta) % Duration;
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
			Nodes.Add(child);
			AddChild(child);
		}

		public void Play() {
			Playing = true;
		}
	}
}