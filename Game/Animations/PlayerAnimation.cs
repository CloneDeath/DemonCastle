using DemonCastle.Projects.Data;
using Godot;
using Godot.Collections;

namespace DemonCastle.Game.Animations {
	public class PlayerAnimation : Node2D {
		protected readonly CharacterInfo Character;
		protected Dictionary<string, AnimationNode> Animations { get; } = new Dictionary<string, AnimationNode>();
		protected AnimationNode CurrentAnimation;
		public PlayerAnimation(CharacterInfo character) {
			Character = character;
			foreach (var animation in character.Animations) {
				var animationNode = new AnimationNode(animation);
				Animations[animationNode.Name] = animationNode;
				AddChild(animationNode);
			}
		}
		
		public void PlayIdle() => Play(Character.IdleAnimation);
		public void PlayWalk() => Play(Character.WalkAnimation);

		protected void Play(string animationName) {
			if (CurrentAnimation?.Name == animationName) return;
			if (CurrentAnimation != null) {
				CurrentAnimation.Visible = false;
			}
			
			CurrentAnimation = Animations[animationName];
			CurrentAnimation.Visible = true;
			CurrentAnimation.Play();
		}
	}
}