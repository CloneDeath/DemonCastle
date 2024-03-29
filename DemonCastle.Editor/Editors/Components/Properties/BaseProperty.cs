using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public interface IBaseProperty {
	void Enable();
	void Disable();
}

public partial class BaseProperty : HBoxContainer, IBaseProperty {
	protected Label Label { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public BaseProperty() {
		Name = nameof(BaseProperty);
		AddChild(Label = new Label());
	}

	public virtual void Enable() {}
	public virtual void Disable() {}
}