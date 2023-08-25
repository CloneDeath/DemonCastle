using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class FileTree {
		protected DirectoryNavigator Root { get; }
		protected DirectoryPopupMenu DirectoryPopupMenu { get; }
		protected FilePopupMenu FilePopupMenu { get; }
		protected DeleteDialog ConfirmDelete { get; }
		protected RenameDialog ConfirmRename { get; }

		
		public FileTree(DirectoryNavigator rootDirectory) {
			Name = nameof(FileTree);
			Root = rootDirectory;
			HideRoot = true;
			AllowRmbSelect = true;

			AddChild(ConfirmRename = new RenameDialog());
			ConfirmRename.Connect(AcceptDialog.SignalName.Confirmed, new Callable(this, nameof(OnRenameConfirmed)));
			
			AddChild(ConfirmDelete = new DeleteDialog());
			ConfirmDelete.Connect(AcceptDialog.SignalName.Confirmed, new Callable(this, nameof(OnDeleteConfirmed)));

			AddChild(DirectoryPopupMenu = new DirectoryPopupMenu());
			DirectoryPopupMenu.CreateCharacter += OnCreateCharacterSelected;

			AddChild(FilePopupMenu = new FilePopupMenu());
			FilePopupMenu.RenameFile += OnRenameFile;
			FilePopupMenu.DeleteFile += OnDeleteFile;

			CreateTree();
			this.ItemActivated += this.FileActivated;
			this.ItemSelected += this.OnItemSelected;
		}
	}
}