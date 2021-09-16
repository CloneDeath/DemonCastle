using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character {
	public class AnimationArea : Control {
		protected AnimationList Animations { get; }
		protected SplitContainer SplitContainer { get; }
		public AnimationArea(CharacterInfo characterInfo) {
			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				MarginRight = 0,
				AnchorBottom = 1,
				MarginBottom = 1
			});
			SplitContainer.AddChild(Animations = new AnimationList(characterInfo));
			SplitContainer.AddChild(new SingleAnimationEditArea());
			SplitContainer.SplitOffset = 100;
		}
	}
}