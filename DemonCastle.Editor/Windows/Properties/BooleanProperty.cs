using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class BooleanProperty : BaseProperty {
		protected IPropertyBinding<bool> Binding { get; }
		protected CheckBox CheckBox { get; }

		public bool PropertyValue {
			get => CheckBox.ButtonPressed;
			set => CheckBox.ButtonPressed = value;
		}
		
		public BooleanProperty(IPropertyBinding<bool> binding) {
			Name = nameof(BooleanProperty);
			Binding = binding;
			
			AddChild(CheckBox = new CheckBox {
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				ButtonPressed = Binding.Get()
			});

			CheckBox.Connect("toggled", new Callable(this, nameof(OnValueChange)));
		}

		protected void OnValueChange(bool value) {
			Binding.Set(value);
		}
	}
}