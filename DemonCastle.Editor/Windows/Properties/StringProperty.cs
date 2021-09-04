using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class StringProperty : BaseProperty {
		protected IPropertyBinding<string> Binding { get; }
		protected LineEdit LineEdit { get; }

		public string PropertyValue {
			get => LineEdit.Text;
			set { 
				LineEdit.Text = value;
				OnValueChange(value);
			}
		}

		public StringProperty(IPropertyBinding<string> binding) {
			Name = nameof(StringProperty);
			Binding = binding;

			AddChild(LineEdit = new LineEdit {
				RectMinSize = new Vector2(20, 20),
				Editable = false,
				// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
				SizeFlagsHorizontal = (int)(SizeFlags.Fill | SizeFlags.Expand),
				Text = binding.Get()
			});
			LineEdit.Connect("text_changed", this, nameof(OnValueChange));
		}
		
		protected void OnValueChange(string value) {
			Binding.Set(value);
		}
	}
}