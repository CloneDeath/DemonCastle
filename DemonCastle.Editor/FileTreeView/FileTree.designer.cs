using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class FileTree {
		protected DirectoryNavigator Root { get; }
		protected DirectoryPopupMenu DirectoryPopupMenu { get; }
		protected FilePopupMenu FilePopupMenu { get; }
		protected ConfirmationDialog ConfirmDelete { get; }
		protected RenameDialog ConfirmRename { get; }

		
		public FileTree(DirectoryNavigator rootDirectory) {
			Name = nameof(FileTree);
			Root = rootDirectory;
			HideRoot = true;
			AllowRmbSelect = true;

			AddChild(ConfirmRename = new RenameDialog());
			ConfirmRename.Connect("confirmed", this, nameof(OnRenameConfirmed));
			
			AddChild(ConfirmDelete = new ConfirmationDialog {
				DialogText = "Are you sure you wish to delete this file?",
				PopupExclusive = true
			});
			ConfirmDelete.Connect("confirmed", this, nameof(OnDeleteConfirmed));

			AddChild(DirectoryPopupMenu = new DirectoryPopupMenu());
			DirectoryPopupMenu.CreateCharacter += OnCreateCharacterSelected;

			AddChild(FilePopupMenu = new FilePopupMenu());
			FilePopupMenu.RenameFile += OnRenameFile;
			FilePopupMenu.DeleteFile += OnDeleteFile;

			CreateTree();
			Connect("item_activated", this, nameof(ItemActivated));
			Connect("item_rmb_selected", this, nameof(ItemRmbSelected));
		}
	}
}