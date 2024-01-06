using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Color;

public partial class ColorProperty : BaseProperty {
	protected IPropertyBinding<Godot.Color> Binding { get; }
	protected ColorPickerButton ColorPicker { get; }

	public Godot.Color PropertyValue {
		get => ColorPicker.Color;
		set => ColorPicker.Color = value;
	}

	public ColorProperty(IPropertyBinding<Godot.Color> binding, ColorPropertyOptions options) {
		Name = nameof(ColorProperty);
		Binding = binding;

		AddChild(ColorPicker = new ColorPickerButton {
			CustomMinimumSize = new Vector2(24, 24),
			Color = binding.Get(),
			EditAlpha = options.EditAlpha
		});
		ColorPicker.ColorChanged += ColorPicker_OnColorChanged;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(Godot.Color value) {
		PropertyValue = value;
	}

	private void ColorPicker_OnColorChanged(Godot.Color color) {
		Binding.Set(color);
	}

	public override void Enable() {
		base.Enable();
		ColorPicker.Disabled = false;
	}

	public override void Disable() {
		base.Disable();
		ColorPicker.Disabled = true;
	}
}