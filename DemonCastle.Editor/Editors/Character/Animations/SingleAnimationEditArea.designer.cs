using DemonCastle.Editor.Controls;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations {
	public partial class SingleAnimationEditArea {
		protected BindingLineEdit LineEdit { get; }
		protected Button AddFrameButton { get; }
		protected AnimationFrameContainer FrameContainer { get; }

		public SingleAnimationEditArea() {
			AddChild(LineEdit = new BindingLineEdit());
			AddChild(AddFrameButton = new Button {
				Text = "Add Frame"
			});
			AddFrameButton.Pressed += this.OnAddFrameButtonPressed;
			AddChild(FrameContainer = new AnimationFrameContainer());
		}
	}
}