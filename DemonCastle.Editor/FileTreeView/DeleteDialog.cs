using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public class DeleteDialog : ConfirmationDialog {
		public DeleteDialog() {
			Name = nameof(DeleteDialog);
			DialogText = "Are you sure you wish to delete this file?";
			PopupExclusive = true;
		}
	}
}