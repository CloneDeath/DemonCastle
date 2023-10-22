using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class FloatProperty : BaseProperty {
	protected IPropertyBinding<float> Binding { get; }
	protected SpinBox SpinBox { get; }

	public float PropertyValue {
		get => (float)SpinBox.Value;
		set => SpinBox.Value = value;
	}

	public FloatProperty(IPropertyBinding<float> binding) {
		Name = nameof(FloatProperty);
		Binding = binding;

		AddChild(SpinBox = new SpinBox {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Step = 0.01,
			Rounded = false,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get()
		});

		SpinBox.ValueChanged += OnValueChange;
	}

	protected void OnValueChange(double value) {
		Binding.Set((float)value);
	}
}