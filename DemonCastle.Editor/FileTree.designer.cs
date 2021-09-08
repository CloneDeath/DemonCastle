using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor {
	public partial class FileTree {
		protected DirectoryNavigator Root { get; }
		protected DirectoryPopupMenu DirectoryPopupMenu { get; }
		
		public FileTree(DirectoryNavigator rootDirectory) {
			Name = nameof(FileTree);
			Root = rootDirectory;
			HideRoot = true;
			AllowRmbSelect = true;

			AddChild(DirectoryPopupMenu = new DirectoryPopupMenu());
			DirectoryPopupMenu.CreateCharacter += OnCreateCharacterSelected;
			
			CreateTree();
			Connect("item_activated", this, nameof(ItemActivated));
			Connect("item_rmb_selected", this, nameof(ItemRmbSelected));
		}
	}
}