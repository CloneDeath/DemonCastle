using System;
using System.Collections.Generic;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class FileTree : Tree {
		public event Action<FileNavigator>? OnFileActivated;

		protected Dictionary<TreeItem, FileNavigator> FileMap { get; } = new();
		protected Dictionary<TreeItem, DirectoryNavigator> DirectoryMap { get; } = new();
		
		protected void FileActivated() {
			var selected = GetSelected();
			if (!FileMap.ContainsKey(selected)) return;
			
			OnFileActivated?.Invoke(FileMap[selected]);
		}

		protected void OnItemSelected() {
			if (!Input.IsMouseButtonPressed(MouseButton.Right)) return;
			var selected = GetSelected();
			if (DirectoryMap.ContainsKey(selected)) {
				DirectoryPopupMenu.Position = (Vector2I)GetViewport().GetMousePosition();
				DirectoryPopupMenu.Popup();
			} else if (FileMap.ContainsKey(selected)) {
				FilePopupMenu.Position = (Vector2I)GetViewport().GetMousePosition();
				FilePopupMenu.Popup();
			}
		}

		protected void CreateTree() {
			Clear();
			FileMap.Clear();
			DirectoryMap.Clear();
			CreateDirectory(null, Root);
		}

		protected void CreateDirectory(TreeItem? parent, DirectoryNavigator directory) {
			if (directory.DirectoryName.StartsWith(".")) return;
			
			var dir = CreateItem(parent);
			dir.SetText(0, directory.DirectoryName);
			dir.SetIcon(0, IconTextures.FolderIcon);
			DirectoryMap[dir] = directory;

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

		protected Texture2D GetIconForFile(string extension) {
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

		public void OnAddDirectorySelected() {
			var selected = GetSelected();
			if (!DirectoryMap.ContainsKey(selected)) return;
			var dirNav = DirectoryMap[selected];
			dirNav.CreateDirectory("directory");
			dirNav.CreateFile("character", "dcc", new CharacterFile {
				Name = "character"
			});
			
			CreateTree();
		}
		
		public void OnCreateCharacterFileSelected() {
			var selected = GetSelected();
			if (!DirectoryMap.ContainsKey(selected)) return;
			var dirNav = DirectoryMap[selected];
			dirNav.CreateFile("character", "dcc", new CharacterFile {
				Name = "character"
			});
			
			CreateTree();
		}
		
		public void OnCreateTextFileSelected() {
			var selected = GetSelected();
			if (!DirectoryMap.ContainsKey(selected)) return;
			var dirNav = DirectoryMap[selected];
			dirNav.CreateFile("text", "txt", string.Empty);
			
			CreateTree();
		}

		protected FileNavigator? SelectedFile {
			get {
				var selected = GetSelected();
				return FileMap.TryGetValue(selected, out var value) ? value : null;
			}
		}

		protected void OnRenameFile() {
			if (SelectedFile == null) return;
			ConfirmRename.Text = SelectedFile.FileNameWithoutExtension;
			ConfirmRename.PopupCentered();
			ConfirmRename.FocusLineEdit();
		}

		protected void OnRenameConfirmed() {
			if (SelectedFile == null) return;

			SelectedFile.Rename($"{ConfirmRename.Text}{SelectedFile.Extension}");
			CreateTree();
		}

		protected void OnDeleteFile() {
			ConfirmDelete.PopupCentered();
		}

		protected void OnDeleteConfirmed() {
			if (SelectedFile == null) return;
			
			SelectedFile.DeleteFile();
			CreateTree();
		}
	}
}