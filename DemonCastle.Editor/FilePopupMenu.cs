using System;
using Godot;

namespace DemonCastle.Editor {
	public class FilePopupMenu : PopupMenu {
		protected ConfirmationDialog ConfirmationDialog { get; }
		
		public event Action DeleteFile;
		
		public FilePopupMenu() {
			Name = nameof(FilePopupMenu);
			AddChild(ConfirmationDialog = new ConfirmationDialog {
				DialogText = "Are you sure you wish to delete this file?",
				PopupExclusive = true
			});
			ConfirmationDialog.Connect("confirmed", this, nameof(DeleteConfirmed));

			AddItem("Delete", 0);

			Connect("id_pressed", this, nameof(OnIdPressed));
		}

		protected void OnIdPressed(int id) {
			ConfirmationDialog.Popup_();
		}

		protected void DeleteConfirmed() {
			DeleteFile?.Invoke();
		}
	}
}