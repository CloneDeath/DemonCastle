using System;
using System.Collections.Generic;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class CharacterAnimation : Node2D {
	public bool IsComplete { get; private set; }
	public Guid AnimationId => CurrentAnimation?.AnimationId ?? Guid.Empty;

	protected readonly CharacterInfo Character;
	protected Dictionary<Guid, AnimationInfoNode> Animations { get; } = new();

	protected AnimationInfoNode? CurrentAnimation;

	public IFrameInfo? CurrentFrame => CurrentAnimation?.CurrentFrame;

	public CharacterAnimation(CharacterInfo character, IDamageable owner, DebugState debug) {
		Name = nameof(CharacterAnimation);
		Character = character;

		foreach (var animation in character.Animations) {
			var animationNode = new AnimationInfoNode(animation, owner, debug) {
				Visible = false
			};
			animationNode.Complete += AnimationNode_OnComplete;
			Animations[animationNode.AnimationId] = animationNode;
			AddChild(animationNode);
		}
		PlayIdle();
	}

	public void PlayIdle() => Play(Character.IdleAnimation);
	public void PlayWalk() => Play(Character.WalkAnimation);
	public void PlayJump() => Play(Character.JumpAnimation);
	public void PlayCrouch() => Play(Character.CrouchAnimation);
	public void PlayStairsUp() => Play(Character.StairsUpAnimation);
	public void PlayStairsDown() => Play(Character.StairsDownAnimation);
	public void PlayStandAttack() => Play(Character.StandAttackAnimation);
	public void PlayJumpAttack() => Play(Character.JumpAttackAnimation);
	public void PlayStairsUpAttack() => Play(Character.StairsUpAttackAnimation);
	public void PlayStairsDownAttack() => Play(Character.StairsDownAttackAnimation);

	protected void Play(Guid animationId) {
		IsComplete = false;
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Visible = false;
		}

		CurrentAnimation = Animations[animationId];
		CurrentAnimation.Visible = true;
		CurrentAnimation.Play();
	}

	private void AnimationNode_OnComplete(AnimationInfoNode infoNode) {
		if (infoNode.AnimationId != CurrentAnimation?.AnimationId) return;
		IsComplete = true;
	}
}