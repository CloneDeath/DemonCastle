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
			ConfirmRename.Confirmed += this.OnRenameConfirmed;
			
			AddChild(ConfirmDelete = new DeleteDialog());
			ConfirmDelete.Confirmed += this.OnDeleteConfirmed;

			AddChild(DirectoryPopupMenu = new DirectoryPopupMenu());
			DirectoryPopupMenu.AddDirectory += OnAddDirectorySelected;
			DirectoryPopupMenu.CreateCharacterFile += OnCreateCharacterFileSelected;
			DirectoryPopupMenu.CreateTextFile += OnCreateTextFileSelected;
			DirectoryPopupMenu.RenameDirectory += OnRename;
			DirectoryPopupMenu.DeleteDirectory += OnDelete;

			AddChild(FilePopupMenu = new FilePopupMenu());
			FilePopupMenu.RenameFile += OnRename;
			FilePopupMenu.DeleteFile += OnDelete;

			CreateTree();
			this.ItemActivated += this.FileActivated;
			this.ItemMouseSelected += this.OnItemSelected;
		}
	}
}