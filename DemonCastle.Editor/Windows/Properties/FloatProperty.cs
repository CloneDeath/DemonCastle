using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class FloatProperty : BaseProperty {
		protected IPropertyBinding<float> Binding { get; }
		protected SpinBox SpinBox { get; }

		public int PropertyValue {
			get => (int)SpinBox.Value;
			set => SpinBox.Value = value;
		}
		
		public FloatProperty(IPropertyBinding<float> binding) {
			Name = nameof(FloatProperty);
			Binding = binding;
			
			AddChild(SpinBox = new SpinBox {
				RectMinSize = new Vector2(20, 20),
				SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
				Step = 0.01,
				Rounded = false,
				Value = Binding.Get()
			});

			SpinBox.Connect("value_changed", this, nameof(OnValueChange));
		}

		protected void OnValueChange(float value) {
			Binding.Set(value);
		}
		
	}
}