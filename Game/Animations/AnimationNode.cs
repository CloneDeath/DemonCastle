using System;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Game.Animations {
	public class AnimationNode : Node2D {
		protected AnimationInfo Animation { get; }

		public AnimationNode(AnimationInfo animation) {
			Animation = animation;
		}

		public string AnimationName => Animation.Name;

		public void Play() {
		}
	}
}