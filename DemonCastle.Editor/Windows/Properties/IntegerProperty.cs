using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class IntegerProperty : BaseProperty {
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
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Value = Binding.Get(),
				Rounded = true
			});
			SpinBox.ValueChanged += OnValueChange;
		}

		protected void OnValueChange(double value) {
			Binding.Set((int)value);
		}
	}
}