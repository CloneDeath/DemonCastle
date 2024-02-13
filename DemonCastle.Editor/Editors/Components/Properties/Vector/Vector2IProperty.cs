using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Vector;

public partial class Vector2IProperty : BoxContainer, IBaseProperty {
	protected Label Label { get; }
	protected IPropertyBinding<Vector2I> Binding { get; }
	protected BoxContainer BoxContainer { get; }
	protected SpinBox XBox { get; }
	protected SpinBox YBox { get; }

	public string DisplayName {
		get => Label.Text;
		set {
			Label.Text = value;
			Label.Visible = !string.IsNullOrWhiteSpace(value);
		}
	}

	public Vector2I PropertyValue {
		get => new((int)XBox.Value, (int)YBox.Value);
		set {
			if ((int)XBox.Value != value.X) XBox.Value = value.X;
			if ((int)YBox.Value != value.Y) YBox.Value = value.Y;
		}
	}

	public Vector2IProperty(IPropertyBinding<Vector2I> binding, Vector2IPropertyOptions options) {
		Name = nameof(BooleanProperty);
		Binding = binding;
		Vertical = !options.InlineLabel;

		AddChild(Label = new Label {
			Visible = false
		});

		AddChild(BoxContainer = options.Vertical ? new VBoxContainer() : new HBoxContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});

		var xContainer = new HBoxContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		BoxContainer.AddChild(xContainer);
		{
			xContainer.AddChild(new Label {
				Text = "X",
				Modulate = Colors.LightGreen
			});
			xContainer.AddChild(XBox = new SpinBox {
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Rounded = true,
				MinValue = options.AllowNegative ? int.MinValue : 0,
				MaxValue = int.MaxValue,
				Value = Binding.Get().X
			});
			XBox.ValueChanged += OnXValueChange;
		}

		var yContainer = new HBoxContainer{
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		BoxContainer.AddChild(yContainer);
		{
			yContainer.AddChild(new Label {
				Text = "Y",
				Modulate = Godot.Color.Color8(0xee, 0x90, 0x90)
			});

			yContainer.AddChild(YBox = new SpinBox {
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Rounded = true,
				MinValue = options.AllowNegative ? int.MinValue : 0,
				MaxValue = int.MaxValue,
				Value = Binding.Get().Y
			});
			YBox.ValueChanged += OnYValueChange;
		}
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
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

	public virtual void Enable() {
		XBox.Editable = true;
		YBox.Editable = true;
	}

	public virtual void Disable() {
		XBox.Editable = false;
		YBox.Editable = false;
	}
}