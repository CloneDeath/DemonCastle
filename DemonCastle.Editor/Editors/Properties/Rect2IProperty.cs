using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class Rect2IProperty : VBoxContainer, IBaseProperty {
	protected Label Label { get; }
	protected IPropertyBinding<Rect2I> Binding { get; }
	protected HBoxContainer HBoxContainer { get; }
	protected SpinBox XBox { get; }
	protected SpinBox YBox { get; }
	protected SpinBox WidthBox { get; }
	protected SpinBox HeightBox { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public Rect2I PropertyValue {
		get => new((int)XBox.Value, (int)YBox.Value, (int)WidthBox.Value, (int)HeightBox.Value);
		set {
			XBox.Value = value.Position.X;
			YBox.Value = value.Position.Y;
			WidthBox.Value = value.Size.X;
			HeightBox.Value = value.Size.Y;
		}
	}

	public Rect2IProperty(IPropertyBinding<Rect2I> binding) {
		Name = nameof(BooleanProperty);
		Binding = binding;

		AddChild(Label = new Label());

		AddChild(HBoxContainer = new HBoxContainer());
		HBoxContainer.AddChild(new Label { Text = "X" });
		HBoxContainer.AddChild(XBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().Position.X
		});
		XBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "Y" });
		HBoxContainer.AddChild(YBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().Position.Y
		});
		YBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "W" });
		HBoxContainer.AddChild(WidthBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().Size.X
		});
		WidthBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "H" });
		HBoxContainer.AddChild(HeightBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Rounded = true,
			MinValue = 0,
			MaxValue = double.MaxValue,
			Value = Binding.Get().Size.Y
		});
		HeightBox.ValueChanged += OnValueChange;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(Rect2I obj) {
		PropertyValue = obj;
	}

	protected void OnValueChange(double value) {
		Binding.Set(PropertyValue);
	}

	public virtual void Enable() {
		XBox.Editable = true;
		YBox.Editable = true;
		WidthBox.Editable = true;
		HeightBox.Editable = true;
	}

	public virtual void Disable() {
		XBox.Editable = false;
		YBox.Editable = false;
		WidthBox.Editable = false;
		HeightBox.Editable = false;
	}
}