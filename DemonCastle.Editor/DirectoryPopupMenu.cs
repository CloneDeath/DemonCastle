using Godot;

namespace DemonCastle.Editor {
	public class DirectoryPopupMenu : PopupMenu {
		public DirectoryPopupMenu() {
			Name = nameof(DirectoryPopupMenu);

			AddItem("Create Character", 0);
		}
	}
}