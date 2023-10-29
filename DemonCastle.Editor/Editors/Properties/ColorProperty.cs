using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class ColorProperty : BaseProperty {
	protected IPropertyBinding<Color> Binding { get; }
	protected ColorPickerButton ColorPicker { get; }

	public Color PropertyValue {
		get => ColorPicker.Color;
		set => ColorPicker.Color = value;
	}

	public ColorProperty(IPropertyBinding<Color> binding) {
		Name = nameof(ColorProperty);
		Binding = binding;

		AddChild(ColorPicker = new ColorPickerButton {
			CustomMinimumSize = new Vector2(24, 24),
			Color = binding.Get()
		});
		ColorPicker.ColorChanged += ColorPicker_OnColorChanged;
	}

	private void ColorPicker_OnColorChanged(Color color) {
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