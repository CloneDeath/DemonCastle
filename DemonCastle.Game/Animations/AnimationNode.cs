using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Game.Animations {
	public partial class AnimationNode : Node2D {
		protected AnimationInfo Animation { get; }
		protected PhasingNode Frames { get; }

		public AnimationNode(AnimationInfo animation) {
			Animation = animation;

			AddChild(Frames = new PhasingNode {
				Duration = Animation.Frames.Sum(f => f.Duration)
			});
			float totalOffset = 0;
			foreach (var frame in Animation.Frames) {
				Frames.AddPhase(frame.Sprite2D, totalOffset, totalOffset + frame.Duration);
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