using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class DirectoryPopupMenu : PopupMenu {
		public event Action? AddDirectory;
		public event Action? CreateCharacterFile;
		public event Action? CreateTextFile;
		
		public DirectoryPopupMenu() {
			Name = nameof(DirectoryPopupMenu);

			AddItem("Add Directory", 0);
			SetItemIcon(0, IconTextures.FolderIcon);
			
			AddItem("Create Character File", 1);
			SetItemIcon(1, IconTextures.CharacterIcon);
			
			AddItem("Create Text File", 2);
			SetItemIcon(2, IconTextures.TextFileIcon);

			IdPressed += OnIdPressed;
		}

		protected void OnIdPressed(long id) {
			switch (id) {
				case 0:
					AddDirectory?.Invoke();
					break;
				case 1:
					CreateCharacterFile?.Invoke();
					break;
				case 2:
					CreateTextFile?.Invoke();
					break;
				default: throw new NotImplementedException($"Id {id} not handled in DirectoryPopupMenu.");
			}
		}
	}
}