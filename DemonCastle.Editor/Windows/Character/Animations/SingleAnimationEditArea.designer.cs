using DemonCastle.Editor.Controls;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class SingleAnimationEditArea {
		protected AnimationFrameGridContainer GridContainer { get; }
		protected BindingLineEdit LineEdit { get; }
		protected Button AddFrameButton { get; }

		public SingleAnimationEditArea() {
			AddChild(LineEdit = new BindingLineEdit());
			AddChild(AddFrameButton = new Button {
				Text = "Add Frame"
			});
			AddFrameButton.Connect("pressed", new Callable(this, nameof(OnAddFrameButtonPressed)));
			AddChild(GridContainer = new AnimationFrameGridContainer {
				Columns = 3
			});
		}
	}
}