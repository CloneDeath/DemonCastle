using Godot;

namespace DemonCastle.Editor.Controls {
	public class WrapperControl<T> : Control
		where T : Control, new() {
		protected T Inner { get; }

		public WrapperControl() {
			AddChild(Inner = new T {
				AnchorRight = 1,
				MarginRight = 0,
				AnchorBottom = 1,
				MarginBottom = 0
			});
		}
	}
}