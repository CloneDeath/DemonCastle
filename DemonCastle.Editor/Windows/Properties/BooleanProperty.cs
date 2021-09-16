using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class BooleanProperty : BaseProperty {
		protected IPropertyBinding<bool> Binding { get; }
		protected CheckBox CheckBox { get; }

		public bool PropertyValue {
			get => CheckBox.Pressed;
			set => CheckBox.Pressed = value;
		}
		
		public BooleanProperty(IPropertyBinding<bool> binding) {
			Name = nameof(BooleanProperty);
			Binding = binding;
			
			AddChild(CheckBox = new CheckBox {
				RectMinSize = new Vector2(20, 20),
				SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
				Pressed = Binding.Get()
			});

			CheckBox.Connect("toggled", this, nameof(OnValueChange));
		}

		protected void OnValueChange(bool value) {
			Binding.Set(value);
		}
	}
}