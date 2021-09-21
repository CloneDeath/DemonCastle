using DemonCastle.Editor.Controls;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class SingleAnimationEditArea : VBoxContainer {
		protected GridContainer GridContainer { get; }
		protected BindingLineEdit LineEdit { get; }

		protected AnimationInfo Current;
		public AnimationInfo CurrentAnimation {
			get => Current;
			set {
				Current = value;
				LineEdit.Binding = new PropertyBinding<AnimationInfo, string>(Current, animation => animation.Name);
			}
		}

		public SingleAnimationEditArea() {
			AddChild(LineEdit = new BindingLineEdit());
			AddChild(GridContainer = new GridContainer {
				Columns = 3
			});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
			GridContainer.AddChild(new Panel{RectMinSize = new Vector2(50, 50)});
		}
	}
}