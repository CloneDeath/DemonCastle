using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Game.Animations;

public partial class PlayerAnimation : Node2D {
	protected readonly CharacterInfo Character;
	protected Dictionary<Guid, AnimationNode> Animations { get; } = new();
	protected AnimationNode? CurrentAnimation;
	public PlayerAnimation(CharacterInfo character) {
		Character = character;
		foreach (var animation in character.Animations) {
			var animationNode = new AnimationNode(animation) {
				Visible = false
			};
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

	protected void Play(Guid animationId) {
		if (CurrentAnimation?.AnimationId == animationId) return;
		if (CurrentAnimation != null) {
			CurrentAnimation.Visible = false;
		}

		CurrentAnimation = Animations[animationId];
		CurrentAnimation.Visible = true;
		CurrentAnimation.Play();
	}
}