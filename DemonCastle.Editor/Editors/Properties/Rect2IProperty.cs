using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class Rect2IProperty : VBoxContainer {
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
		binding.Changed += Binding_OnChanged;

		AddChild(Label = new Label());

		AddChild(HBoxContainer = new HBoxContainer());
		HBoxContainer.AddChild(new Label { Text = "X" });
		HBoxContainer.AddChild(XBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Value = Binding.Get().Position.X,
			Rounded = true
		});
		XBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "Y" });
		HBoxContainer.AddChild(YBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Value = Binding.Get().Position.Y,
			Rounded = true
		});
		YBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "W" });
		HBoxContainer.AddChild(WidthBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Value = Binding.Get().Size.X,
			Rounded = true
		});
		WidthBox.ValueChanged += OnValueChange;

		HBoxContainer.AddChild(new Label { Text = "H" });
		HBoxContainer.AddChild(HeightBox = new SpinBox {
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Value = Binding.Get().Size.Y,
			Rounded = true
		});
		HeightBox.ValueChanged += OnValueChange;
	}

	private void Binding_OnChanged(Rect2I obj) {
		PropertyValue = obj;
	}

	protected void OnValueChange(double value) {
		Binding.Set(PropertyValue);
	}
}