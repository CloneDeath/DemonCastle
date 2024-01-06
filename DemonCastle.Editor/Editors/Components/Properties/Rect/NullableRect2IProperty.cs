using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Rect;

public partial class NullableRect2IProperty : VBoxContainer, IBaseProperty{
	protected CheckBox CheckBox { get; }
	protected IPropertyBinding<Rect2I?> Binding { get; }
	protected HBoxContainer HBoxContainer { get; }
	protected SpinBox XBox { get; }
	protected SpinBox YBox { get; }
	protected SpinBox WidthBox { get; }
	protected SpinBox HeightBox { get; }

	public string DisplayName {
		get => CheckBox.Text;
		set => CheckBox.Text = value;
	}

	public Rect2I? PropertyValue {
		get => CheckBox.ButtonPressed ? new Rect2I((int)XBox.Value, (int)YBox.Value, (int)WidthBox.Value, (int)HeightBox.Value) : null;
		set {
			if (value == null) {
				CheckBox.ButtonPressed = false;
				RefreshEditable();
				return;
			}

			CheckBox.ButtonPressed = true;
			RefreshEditable();
			XBox.Value = value.Value.Position.X;
			YBox.Value = value.Value.Position.Y;
			WidthBox.Value = value.Value.Size.X;
			HeightBox.Value = value.Value.Size.Y;
		}
	}

	public NullableRect2IProperty(IPropertyBinding<Rect2I?> binding) {
		Name = nameof(BooleanProperty);
		Binding = binding;

		AddChild(CheckBox = new CheckBox());
		CheckBox.Toggled += CheckBox_OnToggled;

		AddChild(HBoxContainer = new HBoxContainer());
		HBoxContainer.AddChild(new Label { Text = "X" });
		HBoxContainer.AddChild(XBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get()?.Position.X ?? 0
		});
		XBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "Y" });
		HBoxContainer.AddChild(YBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get()?.Position.Y ?? 0
		});
		YBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "W" });
		HBoxContainer.AddChild(WidthBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get()?.Size.X ?? 0
		});
		WidthBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "H" });
		HBoxContainer.AddChild(HeightBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get()?.Size.Y ?? 0
		});
		HeightBox.ValueChanged += OnValueChange;
	}

	private void CheckBox_OnToggled(bool buttonPressed) {
		Binding.Set(PropertyValue);
		RefreshEditable();
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(Rect2I? obj) {
		PropertyValue = obj;
	}

	protected void OnValueChange(double value) {
		Binding.Set(PropertyValue);
	}

	public virtual void Enable() {
		CheckBox.Disabled = false;
		RefreshEditable();
	}

	public virtual void Disable() {
		CheckBox.Disabled = true;
		RefreshEditable();
	}

	private void RefreshEditable() {
		var enabled = !CheckBox.Disabled;
		XBox.Editable = enabled && CheckBox.ButtonPressed;
		YBox.Editable = enabled && CheckBox.ButtonPressed;
		WidthBox.Editable = enabled && CheckBox.ButtonPressed;
		HeightBox.Editable = enabled && CheckBox.ButtonPressed;
	}
}