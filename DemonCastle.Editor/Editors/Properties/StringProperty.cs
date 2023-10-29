using DemonCastle.Editor.Controls;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class StringProperty : BaseProperty {
	protected BindingLineEdit LineEdit { get; }

	public string PropertyValue {
		get => LineEdit.Text;
		set => LineEdit.Text = value;
	}

	public StringProperty(IPropertyBinding<string> binding) {
		Name = nameof(StringProperty);

		AddChild(LineEdit = new BindingLineEdit {
			Binding = binding,
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			CustomMinimumSize = new Vector2(0, 32)
		});
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