using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

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

		CheckBox.Toggled += OnValueChange;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(bool value) {
		PropertyValue = value;
	}

	protected void OnValueChange(bool value) {
		Binding.Set(value);
	}

	public override void Enable() {
		base.Enable();
		CheckBox.Disabled = false;
	}

	public override void Disable() {
		base.Disable();
		CheckBox.Disabled = true;
	}
}