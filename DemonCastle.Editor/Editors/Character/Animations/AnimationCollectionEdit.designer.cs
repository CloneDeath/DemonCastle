using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class AnimationCollectionEdit {
		protected AnimationInfoCollection Animations { get; }
		protected AnimationItemList AnimationItems { get; }
		protected Button AddButton { get; }
		protected Button RemoveButton { get; }
		
		public AnimationCollectionEdit(AnimationInfoCollection animations) {
			Animations = animations;
			
			AddChild(AddButton = new Button {
				Text = "Add"
			});
			AddButton.Pressed += this.OnAddPressed;
			
			AddChild(AnimationItems = new AnimationItemList {
				SizeFlagsVertical = SizeFlags.ExpandFill
			});
			AnimationItems.ItemSelected += this.OnAnimationSelected;
			AnimationItems.AddAnimations(animations);
			
			AddChild(RemoveButton = new Button {
				Text = "Remove"
			});
			RemoveButton.Pressed += this.OnRemovePressed;
		}
	}
}