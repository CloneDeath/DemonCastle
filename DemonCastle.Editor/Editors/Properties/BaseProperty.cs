using Godot;

namespace DemonCastle.Editor.Editors.Properties; 

public partial class BaseProperty : HBoxContainer {
	protected Label Label { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}
		
	public BaseProperty() {
		Name = nameof(BaseProperty);
		AddChild(Label = new Label());
	}
}