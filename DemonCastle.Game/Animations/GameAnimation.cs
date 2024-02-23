using System;
using System.Collections.Generic;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class GameAnimation : Node2D {
	private readonly IDamageable _owner;
	private readonly DebugState _debug;
	protected IAnimationInfoCollection? AnimationCollection;
	protected Dictionary<Guid, AnimationInfoNode> Animations { get; } = new();

	protected AnimationInfoNode? CurrentAnimation;

	public FrameInfo? CurrentFrame => CurrentAnimation?.CurrentFrame as FrameInfo;

	public GameAnimation(IDamageable owner, DebugState debug) {
		_owner = owner;
		_debug = debug;
		Name = nameof(GameAnimation);
	}

	public void PlayNone() => Play(Guid.Empty);
	public void Play(string animationName) => Play(AnimationCollection?.GetAnimationId(animationName) ?? Guid.Empty);
	public void Play(Guid animationId) {
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Active = false;
			CurrentAnimation.Visible = false;
		}

		CurrentAnimation = Animations.GetValueOrDefault(animationId);
		if (CurrentAnimation == null) return;

		CurrentAnimation.Visible = true;
		CurrentAnimation.Active = true;
		CurrentAnimation.Play();
	}

	public void SetAnimation(IAnimationInfoCollection? animations) {
		foreach (var animation in Animations.Values) {
			animation.QueueFree();
		}
		Animations.Clear();
		AnimationCollection = animations;

		if (animations == null) {
			PlayNone();
			return;
		}

		foreach (var animation in animations) {
			var animationNode = new AnimationInfoNode(animation, _owner, _debug) {
				Visible = false
			};
			Animations[animationNode.AnimationId] = animationNode;
			AddChild(animationNode);
		}
		PlayNone();
	}

	public void Reset() => SetAnimation(null);
}