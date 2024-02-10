using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class AnimationInfoNode : Node2D {
	public event Action<AnimationInfoNode>? Complete;
	protected IAnimationInfo Animation { get; }
	protected PhasingNode? Frames { get; }
	protected List<FrameInfoNode> FrameNodes { get; } = new();

	private bool _active;
	public bool Active {
		get => _active;
		set {
			_active = value;
			foreach (var frameNode in FrameNodes) {
				frameNode.AnimationActive = value;
			}
		}
	}

	public AnimationInfoNode(IAnimationInfo animation, IDamageable owner, DebugState debug) {
		Name = $"{nameof(AnimationInfoNode)} {animation.Name}";
		Animation = animation;

		if (Animation.Frames.Count() == 1) {
			var frame = Animation.Frames.First();
			var frameInfoNode = new FrameInfoNode(frame, owner, debug) {
				StartTime = 0,
				EndTime = frame.Duration
			};
			FrameNodes.Add(frameInfoNode);
			AddChild(frameInfoNode);
		} else {
			AddChild(Frames = new PhasingNode {
				Duration = Animation.Frames.Sum(f => f.Duration)
			});

			Frames.Complete += _ => Complete?.Invoke(this);
			float totalOffset = 0;
			foreach (var frame in Animation.Frames) {
				var frameInfoNode = new FrameInfoNode(frame, owner, debug) {
					StartTime = totalOffset,
					EndTime = totalOffset + frame.Duration
				};
				FrameNodes.Add(frameInfoNode);
				Frames.AddPhase(frameInfoNode);
				totalOffset += frame.Duration;
			}
		}
	}

	public float Duration => Animation.Frames.Sum(f => f.Duration);

	public Guid AnimationId => Animation.Id;

	public IFrameInfo? CurrentFrame {
		get {
			if (Frames == null) return Animation.Frames.FirstOrDefault();

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
		if (Frames != null) {
			Frames.CurrentTime = 0;
			Frames.Play();
		} else {
			foreach (var frame in FrameNodes) {
				frame.AnimationActive = true;
				frame.CurrentTime = 0;
			}
		}
	}
}