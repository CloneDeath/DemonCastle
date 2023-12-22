using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public partial class NullableRect2IView : Control {
	private readonly IPropertyBinding<Rect2I?> _binding;
	protected Outline Outline { get; }

	public Color Color {
		get => Outline.Color;
		set => Outline.Color = value;
	}

	public NullableRect2IView(IPropertyBinding<Rect2I?> binding) {
		Name = nameof(NullableRect2IView);

		_binding = binding;

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);

		Binding_OnChanged(binding.Get());
	}

	public override void _EnterTree() {
		base._EnterTree();
		_binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(Rect2I? obj) {
		Visible = obj.HasValue;

		if (!obj.HasValue) return;

		Position = obj.Value.Position;
		Size = obj.Value.Size;
	}
}