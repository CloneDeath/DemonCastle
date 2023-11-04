using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Controls;

public partial class BindingLineEdit : WrapperControl<LineEdit> {
	protected IPropertyBinding<string>? PropertyBinding { get; set; }
	public IPropertyBinding<string>? Binding {
		get => PropertyBinding;
		set {
			if (PropertyBinding != null) {
				PropertyBinding.Changed -= PropertyBinding_OnChanged;
			}
			PropertyBinding = value;
			if (PropertyBinding != null) {
				PropertyBinding.Changed += PropertyBinding_OnChanged;
			}
			Inner.Text = PropertyBinding?.Get();
		}
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (PropertyBinding != null) PropertyBinding.Changed -= PropertyBinding_OnChanged;
	}

	public string Text {
		get => Inner.Text;
		set {
			Inner.Text = value;
			OnValueChange(value);
		}
	}

	public bool Editable {
		get => Inner.Editable;
		set => Inner.Editable = value;
	}

	public BindingLineEdit() {
		Inner.TextChanged += OnValueChange;
		CustomMinimumSize = new Vector2(0, 32);
	}

	protected void OnValueChange(string value) {
		Binding?.Set(value);
	}

	private void PropertyBinding_OnChanged(string value) {
		if (Inner.Text == value) return;
		Inner.Text = value;
	}
}