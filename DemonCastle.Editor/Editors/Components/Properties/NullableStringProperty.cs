using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class NullableStringProperty : BaseProperty {
	protected IPropertyBinding<string?> Binding { get; }
	protected LineEdit LineEdit { get; }

	public string? PropertyValue {
		get => string.IsNullOrEmpty(LineEdit.Text) ? null : LineEdit.Text;
		set => LineEdit.Text = value ?? string.Empty;
	}

	public NullableStringProperty(IPropertyBinding<string?> binding) {
		Name = nameof(FloatProperty);
		Binding = binding;

		AddChild(LineEdit = new LineEdit {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			Text = Binding.Get() ?? string.Empty
		});

		LineEdit.TextChanged += OnTextChange;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(string? value) {
		PropertyValue = value;
	}

	protected void OnTextChange(string text) {
		Binding.Set(PropertyValue);
	}

	public override void Enable() {
		base.Enable();
		LineEdit.Editable = true;
	}

	public override void Disable() {
		base.Disable();
		LineEdit.Editable = false;
	}
}