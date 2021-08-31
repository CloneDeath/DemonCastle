using System;
using System.Collections.Generic;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public class FileTree : Tree {
		protected DirectoryNavigator Root { get; }
		public event Action<FileNavigator> OnItemActivated;

		protected Dictionary<TreeItem, FileNavigator> FileMap { get; } = new Dictionary<TreeItem, FileNavigator>();

		public FileTree(DirectoryNavigator rootDirectory) {
			Name = nameof(FileTree);
			Root = rootDirectory;
			HideRoot = true;
			CreateTree();
			Connect("item_activated", this, nameof(ItemActivated));
		}

		protected void ItemActivated() {
			var selected = GetSelected();
			if (!FileMap.ContainsKey(selected)) return;
			OnItemActivated?.Invoke(FileMap[selected]);
		}

		protected void CreateTree() {
			Clear();
			FileMap.Clear();
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
			FileMap[node] = file;
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