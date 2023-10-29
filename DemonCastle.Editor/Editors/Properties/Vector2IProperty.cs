using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class Vector2IProperty : VBoxContainer {
	protected Label Label { get; }
	protected IPropertyBinding<Vector2I> Binding { get; }
	protected HBoxContainer HBoxContainer { get; }
	protected SpinBox XBox { get; }
	protected SpinBox YBox { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public Vector2I PropertyValue {
		get => new((int)XBox.Value, (int)YBox.Value);
		set {
			XBox.Value = value.X;
			YBox.Value = value.Y;
		}
	}

	public Vector2IProperty(IPropertyBinding<Vector2I> binding) {
		Name = nameof(BooleanProperty);
		Binding = binding;
		Binding.Changed += Binding_OnChanged;

		AddChild(Label = new Label());

		AddChild(HBoxContainer = new HBoxContainer());
		HBoxContainer.AddChild(new Label { Text = "X" });
		HBoxContainer.AddChild(XBox = new SpinBox {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().X
		});
		XBox.ValueChanged += OnXValueChange;

		HBoxContainer.AddChild(new Label { Text = "Y" });
		HBoxContainer.AddChild(YBox = new SpinBox {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().Y
		});
		YBox.ValueChanged += OnYValueChange;
	}

	protected void OnXValueChange(double value) {
		Binding.Set(new Vector2I((int)value, (int)YBox.Value));
	}

	protected void OnYValueChange(double value) {
		Binding.Set(new Vector2I((int)XBox.Value, (int)value));
	}

	private void Binding_OnChanged(Vector2I value) {
		PropertyValue = value;
	}
}