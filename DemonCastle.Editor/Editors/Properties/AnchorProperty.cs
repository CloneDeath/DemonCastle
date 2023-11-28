using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class AnchorProperty : VBoxContainer, IBaseProperty {
	protected Label Label { get; }
	protected IPropertyBinding<Vector2I> Binding { get; }
	protected GridContainer GridContainer { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public Vector2I PropertyValue {
		get => Vector2I.Zero;
		set {
		}
	}

	public AnchorProperty(IPropertyBinding<Vector2I> binding) {
		Name = nameof(BooleanProperty);
		Binding = binding;

		AddChild(Label = new Label());

		AddChild(GridContainer = new GridContainer {
			Columns = 3
		});
		GridContainer.AddChild(new Button {
			Text = "\u2196"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2191"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2197"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2190"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2022"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2192"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2199"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2193"
		});
		GridContainer.AddChild(new Button {
			Text = "\u2198"
		});
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}
	private void Binding_OnChanged(Vector2I value) {
		PropertyValue = value;
	}

	public virtual void Enable() {
	}

	public virtual void Disable() {
	}
}