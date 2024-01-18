using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class IntegerProperty : BaseProperty {
	protected IPropertyBinding<int> Binding { get; }
	protected SpinBox SpinBox { get; }

	public int PropertyValue {
		get => (int)SpinBox.Value;
		set => SpinBox.Value = value;
	}

	public IntegerProperty(IPropertyBinding<int> binding, IntegerPropertyOptions? options = null) {
		Name = nameof(IntegerProperty);
		Binding = binding;

		AddChild(SpinBox = new SpinBox {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			AllowLesser = options?.AllowNegative ?? false,
			MinValue = 0,
			MaxValue = int.MaxValue,
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

	private void Binding_OnChanged(int value) {
		PropertyValue = value;
	}

	protected void OnValueChange(double value) {
		Binding.Set((int)value);
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