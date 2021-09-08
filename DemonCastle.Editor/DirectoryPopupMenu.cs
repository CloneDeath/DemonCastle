using System;
using Godot;

namespace DemonCastle.Editor {
	public class DirectoryPopupMenu : PopupMenu {
		public event Action CreateCharacter;
		
		public DirectoryPopupMenu() {
			Name = nameof(DirectoryPopupMenu);

			AddItem("Create Character", 0);

			Connect("id_pressed", this, nameof(OnIdPressed));
		}

		protected void OnIdPressed(int id) {
			CreateCharacter?.Invoke();
		}
	}
}