using Godot;

namespace DemonCastle.Editor.Controls; 

public partial class WrapperControl<T> : Control
	where T : Control, new() {
	protected T Inner { get; }

	public WrapperControl() {
		AddChild(Inner = new T {
			AnchorRight = 1,
			OffsetRight = 0,
			AnchorBottom = 1,
			OffsetBottom = 0
		});
	}
}