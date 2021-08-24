using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public class FileTree : Tree {
		protected DirectoryNavigator Root { get; }

		public FileTree(DirectoryNavigator rootDirectory) {
			Root = rootDirectory;
			
			CreateTree();
		}

		protected void CreateTree() {
			Clear();
			CreateDirectory(null, Root);
		}

		protected void CreateDirectory(TreeItem parent, DirectoryNavigator directory) {
			if (directory.DirectoryName.StartsWith(".")) return;
			
			var dir = CreateItem(parent);
			dir.SetText(0, directory.DirectoryName);
			
			foreach (var subDirectory in directory.GetDirectories()) {
				CreateDirectory(dir, subDirectory);
			}

			foreach (var file in directory.GetFiles()) {
				CreateFile(dir, file);
			}
		}

		protected void CreateFile(TreeItem parent, FileNavigator file) {
			var node = CreateItem(parent);
			node.SetText(0, file.FileName);
		}
	}
}