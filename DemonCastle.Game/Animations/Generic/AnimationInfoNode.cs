using System;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class AnimationInfoNode : Node2D {
	public event Action<AnimationInfoNode>? Complete;
	protected IAnimationInfo Animation { get; }
	protected PhasingNode Frames { get; }

	public AnimationInfoNode(IAnimationInfo animation) {
		Animation = animation;

		AddChild(Frames = new PhasingNode {
			Duration = Animation.Frames.Sum(f => f.Duration)
		});
		Frames.Complete += _ => Complete?.Invoke(this);
		float totalOffset = 0;
		foreach (var frame in Animation.Frames) {
			var sprite = new SpriteDefinitionNode(frame.SpriteDefinition);
			Frames.AddPhase(sprite, totalOffset, totalOffset + frame.Duration);
			totalOffset += frame.Duration;
		}
	}

	public Guid AnimationId => Animation.Id;

	public IFrameInfo? CurrentFrame {
		get {
			var currentTime = Frames.CurrentTime;
			foreach (var frame in Animation.Frames) {
				if (currentTime < frame.Duration) {
					return frame;
				}
				currentTime -= frame.Duration;
			}

			return null;
		}
	}

	public void Play() {
		Frames.CurrentTime = 0;
		Frames.Play();
	}
}