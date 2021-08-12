using System.Linq;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game.Animations {
	public class AnimationNode : Node2D {
		protected AnimationInfo Animation { get; }
		protected PhasingNode Frames { get; }

		public AnimationNode(AnimationInfo animation) {
			Animation = animation;

			AddChild(Frames = new PhasingNode {
				Duration = Animation.Frames.Sum(f => f.Duration)
			});
			float totalOffset = 0;
			foreach (var frame in Animation.Frames) {
				Frames.AddPhase(frame.Sprite, totalOffset, totalOffset + frame.Duration);
				totalOffset += frame.Duration;
			}
		}

		public string AnimationName => Animation.Name;

		public void Play() {
			Frames.CurrentTime = 0;
			Frames.Play();
		}
	}
}