using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Vector;

public partial class Vector2Property : VBoxContainer, IBaseProperty {
	protected Label Label { get; }
	protected IPropertyBinding<Vector2> Binding { get; }
	protected BoxContainer BoxContainer { get; }
	protected SpinBox XBox { get; }
	protected SpinBox YBox { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public Vector2 PropertyValue {
		get => new((float)XBox.Value, (float)YBox.Value);
		set {
			XBox.SetValueNoSignal(value.X);
			YBox.SetValueNoSignal(value.Y);
		}
	}

	public Vector2Property(IPropertyBinding<Vector2> binding, Vector2PropertyOptions options) {
		Name = nameof(BooleanProperty);
		Binding = binding;

		AddChild(Label = new Label());

		AddChild(BoxContainer = options.Vertical ? new VBoxContainer() : new HBoxContainer());

		var xContainer = new HBoxContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		BoxContainer.AddChild(xContainer);
		{
			xContainer.AddChild(new Label { Text = "X" });
			xContainer.AddChild(XBox = new SpinBox {
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Rounded = false,
				Step = 0.01,
				MinValue = options.KeepPositive ? 0 : int.MinValue,
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
			yContainer.AddChild(new Label { Text = "Y" });
			yContainer.AddChild(YBox = new SpinBox {
				CustomMinimumSize = new Vector2(20, 20),
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Rounded = false,
				Step = 0.01,
				MinValue = options.KeepPositive ? 0 : int.MinValue,
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
		Binding.Set(new Vector2((float)value, (float)YBox.Value));
	}

	protected void OnYValueChange(double value) {
		Binding.Set(new Vector2((float)XBox.Value, (float)value));
	}

	private void Binding_OnChanged(Vector2 value) {
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