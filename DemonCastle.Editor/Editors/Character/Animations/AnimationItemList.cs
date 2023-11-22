using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations; 

public partial class AnimationItemList : ItemList {
	public void AddAnimationLibrary(CharacterAnimationInfo animation) {
		AddItem(animation.Name);
	}

	public void AddAnimations(IEnumerable<CharacterAnimationInfo> animations) {
		foreach (var animation in animations) {
			AddAnimationLibrary(animation);
		}
	}
}