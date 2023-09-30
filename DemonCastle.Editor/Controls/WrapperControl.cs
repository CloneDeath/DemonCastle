using Godot;

namespace DemonCastle.Editor.Controls;

public partial class WrapperControl<T> : Control
	where T : Control, new() {
	protected T Inner { get; }

	public WrapperControl() {
		AddChild(Inner = new T());
		Inner.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}