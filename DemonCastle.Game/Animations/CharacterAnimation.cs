using System;
using System.Collections.Generic;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class CharacterAnimation : Node2D {
	private readonly IDamageable _owner;
	private readonly DebugState _debug;
	public bool IsComplete { get; private set; }
	public Guid AnimationId => CurrentAnimation?.AnimationId ?? Guid.Empty;

	protected CharacterInfo? Character;
	protected Dictionary<Guid, AnimationInfoNode> Animations { get; } = new();

	protected AnimationInfoNode? CurrentAnimation;

	public IFrameInfo? CurrentFrame => CurrentAnimation?.CurrentFrame;

	public float CurrentAnimationDuration => CurrentAnimation?.Duration ?? 0;

	public CharacterAnimation(IDamageable owner, DebugState debug) {
		_owner = owner;
		_debug = debug;
		Name = nameof(CharacterAnimation);
	}

	public void PlayIdle() => Play(Character?.IdleAnimation ?? Guid.Empty);
	public void PlayWalk() => Play(Character?.WalkAnimation ?? Guid.Empty);
	public void PlayJump() => Play(Character?.JumpAnimation ?? Guid.Empty);
	public void PlayCrouch() => Play(Character?.CrouchAnimation ?? Guid.Empty);
	public void PlayStairsUp() => Play(Character?.StairsUpAnimation ?? Guid.Empty);
	public void PlayStairsDown() => Play(Character?.StairsDownAnimation ?? Guid.Empty);
	public void PlayStandAttack() => Play(Character?.StandAttackAnimation ?? Guid.Empty);
	public void PlayJumpAttack() => Play(Character?.JumpAttackAnimation ?? Guid.Empty);
	public void PlayStairsUpAttack() => Play(Character?.StairsUpAttackAnimation ?? Guid.Empty);
	public void PlayStairsDownAttack() => Play(Character?.StairsDownAttackAnimation ?? Guid.Empty);

	protected void Play(Guid animationId) {
		IsComplete = false;
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Visible = false;
			CurrentAnimation.Active = false;
		}

		if (!Animations.TryGetValue(animationId, out var animation)) return;

		CurrentAnimation = animation;
		CurrentAnimation.Visible = true;
		CurrentAnimation.Active = true;
		CurrentAnimation.Play();
	}

	private void AnimationNode_OnComplete(AnimationInfoNode infoNode) {
		if (infoNode.AnimationId != CurrentAnimation?.AnimationId) return;
		IsComplete = true;
	}

	public void SetCharacter(CharacterInfo character) {
		ClearAnimations();
		Character = character;

		foreach (var animation in character.Animations) {
			var animationNode = new AnimationInfoNode(animation, _owner, _debug) {
				Visible = false
			};
			animationNode.Complete += AnimationNode_OnComplete;
			Animations[animationNode.AnimationId] = animationNode;
			AddChild(animationNode);
		}
		PlayIdle();
	}

	public void Reset() {
		ClearAnimations();
		Character = null;
	}

	private void ClearAnimations() {
		foreach (var animation in Animations.Values) {
			animation.QueueFree();
		}
		Animations.Clear();
	}
}