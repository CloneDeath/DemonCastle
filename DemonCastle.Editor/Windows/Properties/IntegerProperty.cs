using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class IntegerProperty : BaseProperty {
		protected IPropertyBinding<int> Binding { get; }
		protected SpinBox SpinBox { get; }

		public int PropertyValue {
			get => (int)SpinBox.Value;
			set => SpinBox.Value = value;
		}
		
		public IntegerProperty(IPropertyBinding<int> binding) {
			Name = nameof(IntegerProperty);
			Binding = binding;
			
			AddChild(SpinBox = new SpinBox {
				RectMinSize = new Vector2(20, 20),
				SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
				Value = Binding.Get()
			});

			SpinBox.Connect("value_changed", this, nameof(OnValueChange));
		}

		protected void OnValueChange(float value) {
			Binding.Set((int)value);
		}
	}
}