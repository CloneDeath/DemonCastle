using DemonCastle.Projects.Data;
using Godot.Collections;

namespace DemonCastle.Game {
	public partial class PlayerAnimation {
		protected Dictionary<string, AnimationNode> Animations { get; } = new Dictionary<string, AnimationNode>();
		
		public PlayerAnimation(CharacterInfo character) {
			foreach (var animation in character.Animations) {
				var animationNode = new AnimationNode(animation);
				Animations[animationNode.Name] = animationNode;
				AddChild(animationNode);
			}
		}
	}
}