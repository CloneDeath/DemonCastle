using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class TextFileWindow : WindowDialog {
		public override void _Process(float delta) {
			base._Process(delta);

			if (RectGlobalPosition.x < GetParentControl().RectGlobalPosition.x) {
				RectGlobalPosition = new Vector2(GetParentControl().RectGlobalPosition.x, RectGlobalPosition.y);
			}

			if (RectGlobalPosition.y < GetParentControl().RectGlobalPosition.y + 20) {
				RectGlobalPosition = new Vector2(RectGlobalPosition.x, GetParentControl().RectGlobalPosition.y + 20);
			}
		}
	}
}