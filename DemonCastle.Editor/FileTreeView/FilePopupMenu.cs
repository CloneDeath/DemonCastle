using System;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public class FilePopupMenu : PopupMenu {
		public event Action RenameFile;
		
		public event Action DeleteFile;
		
		public FilePopupMenu() {
			Name = nameof(FilePopupMenu);

			AddItem("Rename", 0);
			AddItem("Delete", 1);

			Connect("id_pressed", this, nameof(OnIdPressed));
		}

		protected void OnIdPressed(int id) {
			switch (id) {
				case 0: {
					RenameFile?.Invoke();
					break;
				}
				case 1: {
					DeleteFile?.Invoke();
					break;
				}
			}
		}
	}
}