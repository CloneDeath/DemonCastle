using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class AnimationArea : Control {
		protected AnimationCollectionEdit Animations { get; }
		protected SplitContainer SplitContainer { get; }
		protected SingleAnimationEditArea AnimationEdit { get; }
		public AnimationArea(CharacterInfo characterInfo) {
			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				OffsetRight = 0,
				AnchorBottom = 1,
				OffsetBottom = 1
			});
			SplitContainer.AddChild(Animations = new AnimationCollectionEdit(characterInfo.Animations));
			Animations.AnimationSelected += OnAnimationSelected;
			
			SplitContainer.AddChild(AnimationEdit = new SingleAnimationEditArea());
			SplitContainer.SplitOffset = 100;
		}

		protected void OnAnimationSelected(AnimationInfo animationInfo) {
			AnimationEdit.CurrentAnimation = animationInfo;
		}
	}
}