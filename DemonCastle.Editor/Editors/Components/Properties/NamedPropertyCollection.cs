using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class NamedPropertyCollection : PropertyCollection {
	private readonly Label _label;

	public override string DisplayName {
		get => _label.Text;
		set => _label.Text = value;
	}

	public NamedPropertyCollection() {
		AddChild(_label = new Label());
	}
}