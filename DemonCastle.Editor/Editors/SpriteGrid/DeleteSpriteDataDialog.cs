using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid; 

public partial class DeleteSpriteDataDialog : ConfirmationDialog {
	public DeleteSpriteDataDialog() {
		Name = nameof(DeleteSpriteDataDialog);
		DialogText = "Are you sure you wish to delete this sprite data?";
		Exclusive = true;
	}
}