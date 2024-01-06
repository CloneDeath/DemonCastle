using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

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

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(float value) {
		PropertyValue = value;
	}

	protected void OnValueChange(double value) {
		Binding.Set((float)value);
	}

	public override void Enable() {
		base.Enable();
		SpinBox.Editable = true;
	}

	public override void Disable() {
		base.Disable();
		SpinBox.Editable = false;
	}
}