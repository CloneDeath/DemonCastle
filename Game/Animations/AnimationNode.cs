using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game.Animations {
	public class AnimationNode : Node2D {
		protected AnimationInfo Animation { get; }
		protected Tween Tween { get; }

		public AnimationNode(AnimationInfo animation) {
			Animation = animation;

			AddChild(Tween = new Tween {
				Repeat = true
			});
			float totalOffset = 0;
			foreach (var frame in Animation.Frames) {
				var sprite = frame.Sprite;
				AddChild(sprite);
				Tween.InterpolateProperty(sprite, "visible", true, false, frame.Duration
					, Tween.TransitionType.Linear, Tween.EaseType.OutIn, totalOffset);
				totalOffset += frame.Duration;
			}
		}

		public string AnimationName => Animation.Name;

		public void Play() {
			Tween.StopAll();
			Tween.Start();
		}
	}
}