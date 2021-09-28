using DemonCastle.Editor.Controls;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class SingleAnimationEditArea : VBoxContainer {
		protected AnimationFrameGridContainer GridContainer { get; }
		protected BindingLineEdit LineEdit { get; }

		protected AnimationInfo Current;
		public AnimationInfo CurrentAnimation {
			get => Current;
			set {
				Current = value;
				BindAnimation();
			}
		}

		public SingleAnimationEditArea() {
			AddChild(LineEdit = new BindingLineEdit());
			AddChild(GridContainer = new AnimationFrameGridContainer {
				Columns = 3
			});
		}

		protected void BindAnimation() {
			LineEdit.Binding = new PropertyBinding<AnimationInfo, string>(Current, animation => animation.Name);

			GridContainer.ClearChildren();
			foreach (var frame in Current.Frames) {
				GridContainer.AddChild(new AnimationFramePanel(frame));
			}
		}
	}
}