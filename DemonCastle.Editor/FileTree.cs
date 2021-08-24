using DemonCastle.Editor.Icons;
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
			dir.SetIcon(0, IconTextures.FolderIcon);
			
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
			node.SetIcon(0, GetIconForFile(file.Extension));
		}

		protected Texture GetIconForFile(string extension) {
			switch (extension) {
				case ".dcp": return IconTextures.ProjectIcon;
				case ".dcl": return IconTextures.LevelIcon;
				case ".dcsa": return IconTextures.AtlasIcon;
				case ".dcsg": return IconTextures.SpriteGridIcon;
				case ".dcc": return IconTextures.CharacterIcon;
				case ".txt": return IconTextures.TextFileIcon;
				case ".png": return IconTextures.TextureIcon;
				default:
					return IconTextures.UnknownIcon;
			}
		}
	}
}