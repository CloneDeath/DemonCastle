using Godot;

namespace DemonCastle.Editor.FileTreeView.Popups; 

public partial class DeleteDialog : ConfirmationDialog {
	public DeleteDialog() {
		Name = nameof(DeleteDialog);
		DialogText = "Are you sure you wish to delete this file?";
		Exclusive = true;
	}
}