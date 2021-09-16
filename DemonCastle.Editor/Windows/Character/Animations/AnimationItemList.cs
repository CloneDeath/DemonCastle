using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationItemList : ItemList {
		public void AddAnimation(AnimationInfo animation) {
			AddItem(animation.Name);
		}

		public void AddAnimations(IEnumerable<AnimationInfo> animations) {
			foreach (var animation in animations) {
				AddAnimation(animation);
			}
		}
	}
}