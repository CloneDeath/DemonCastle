using System;
using System.Collections.Generic;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class GameAnimation : Node2D {
	protected readonly AnimationInfoCollection AnimationCollection;
	protected Dictionary<Guid, AnimationInfoNode> Animations { get; } = new();

	protected AnimationInfoNode? CurrentAnimation;

	public FrameInfo? CurrentFrame => CurrentAnimation?.CurrentFrame as FrameInfo;

	public GameAnimation(AnimationInfoCollection animations, DebugState debug) {
		Name = nameof(GameAnimation);

		AnimationCollection = animations;
		foreach (var animation in animations) {
			var animationNode = new AnimationInfoNode(animation, debug) {
				Visible = false
			};
			Animations[animationNode.AnimationId] = animationNode;
			AddChild(animationNode);
		}
		PlayNone();
	}

	public void PlayNone() => Play(Guid.Empty);
	public void Play(string animationName) => Play(AnimationCollection.GetAnimationId(animationName));
	public void Play(Guid animationId) {
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Visible = false;
		}

		CurrentAnimation = Animations.GetValueOrDefault(animationId);
		if (CurrentAnimation == null) return;

		CurrentAnimation.Visible = true;
		CurrentAnimation.Play();
	}
}