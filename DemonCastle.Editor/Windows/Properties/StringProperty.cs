using DemonCastle.Editor.Controls;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class StringProperty : BaseProperty {
		protected BindingLineEdit LineEdit { get; }

		public string PropertyValue {
			get => LineEdit.Text;
			set => LineEdit.Text = value;
		}

		public StringProperty(IPropertyBinding<string> binding) {
			Name = nameof(StringProperty);

			AddChild(LineEdit = new BindingLineEdit {
				Binding = binding,
				SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
				RectMinSize = new Vector2(0, 24)
			});
		}
	}
}