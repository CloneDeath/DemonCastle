using DemonCastle.Editor.Windows.Character.Animations;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character {
	public class AnimationArea : Control {
		protected AnimationCollectionEdit Animations { get; }
		protected SplitContainer SplitContainer { get; }
		public AnimationArea(CharacterInfo characterInfo) {
			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				MarginRight = 0,
				AnchorBottom = 1,
				MarginBottom = 1
			});
			SplitContainer.AddChild(Animations = new AnimationCollectionEdit(characterInfo.Animations));
			SplitContainer.AddChild(new SingleAnimationEditArea());
			SplitContainer.SplitOffset = 100;
		}
	}
}