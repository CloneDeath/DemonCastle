using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class NamedPropertyCollection : PropertyCollection {
	private readonly Label Label;

	public override string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public NamedPropertyCollection() {
		AddChild(Label = new Label());
	}
}