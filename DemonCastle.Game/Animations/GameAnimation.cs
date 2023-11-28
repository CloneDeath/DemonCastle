using System;
using System.Collections.Generic;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class GameAnimation : Node2D {
	protected readonly WeaponInfo Weapon;
	protected Dictionary<Guid, AnimationInfoNode> Animations { get; } = new();

	protected AnimationInfoNode? CurrentAnimation;

	public FrameInfo? CurrentFrame => CurrentAnimation?.CurrentFrame as FrameInfo;

	public GameAnimation(WeaponInfo weapon) {
		Weapon = weapon;
		foreach (var animation in weapon.Animations) {
			var animationNode = new AnimationInfoNode(animation) {
				Visible = false
			};
			Animations[animationNode.AnimationId] = animationNode;
			AddChild(animationNode);
		}
		PlayNone();
	}

	public void PlayNone() => Play(Guid.Empty);
	public void Play(string animationName) => Play(Weapon.GetAnimationId(animationName));

	protected void Play(Guid animationId) {
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Visible = false;
		}

		CurrentAnimation = Animations.TryGetValue(animationId, out var animation) ? animation : null;
		if (CurrentAnimation == null) return;

		CurrentAnimation.Visible = true;
		CurrentAnimation.Play();
	}
}