using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationCollectionEdit : VBoxContainer {
		protected AnimationItemList AnimationItems { get; }
		
		public AnimationCollectionEdit(AnimationInfoCollection animations) {
			AddChild(AnimationItems = new AnimationItemList {
				SizeFlagsVertical = (int)SizeFlags.ExpandFill
			});
			AnimationItems.AddAnimations(animations);
		}
	}
}