using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class SingleAnimationEditArea : VBoxContainer {
		protected AnimationInfo Current;
		public AnimationInfo CurrentAnimation {
			get => Current;
			set {
				Current = value;
				BindAnimation();
			}
		}

		protected void BindAnimation() {
			LineEdit.Binding = new PropertyBinding<AnimationInfo, string>(Current, animation => animation.Name);

			GridContainer.ClearChildren();
			foreach (var frame in Current.Frames) {
				GridContainer.AddChild(new AnimationFramePanel(frame));
			}
		}

		protected void OnAddFrameButtonPressed() {
			CurrentAnimation.AddFrame();
			BindAnimation();
		}
	}
}