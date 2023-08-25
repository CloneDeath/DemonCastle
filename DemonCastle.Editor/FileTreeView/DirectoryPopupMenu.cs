using System;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class DirectoryPopupMenu : PopupMenu {
		public event Action? CreateCharacter;
		
		public DirectoryPopupMenu() {
			Name = nameof(DirectoryPopupMenu);

			AddItem("Create Character", 0);

			IdPressed += OnIdPressed;
		}

		protected void OnIdPressed(long id) {
			CreateCharacter?.Invoke();
		}
	}
}