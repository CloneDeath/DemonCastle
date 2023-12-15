using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties.Anchor;

public partial class AnchorProperty : VBoxContainer, IBaseProperty {
	protected Label Label { get; }
	protected IPropertyBinding<Vector2I> Binding { get; }
	protected ArrowGrid ArrowGrid { get; }

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public Vector2I PropertyValue {
		get => ArrowGrid.Value;
		set => ArrowGrid.Value = value;
	}

	public AnchorProperty(IPropertyBinding<Vector2I> binding) {
		Name = nameof(BooleanProperty);
		Binding = binding;

		AddChild(Label = new Label());

		AddChild(ArrowGrid = new ArrowGrid());
		ArrowGrid.ValueChanged += ArrowGrid_OnValueChanged;
	}

	private void ArrowGrid_OnValueChanged(Vector2I value) {
		Binding.Set(value);
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
		ArrowGrid.Enable();
	}

	public virtual void Disable() {
		ArrowGrid.Disable();
	}
}