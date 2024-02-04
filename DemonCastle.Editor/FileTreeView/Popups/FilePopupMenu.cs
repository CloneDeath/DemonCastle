using System;
using Godot;

namespace DemonCastle.Editor.FileTreeView.Popups; 

public partial class FilePopupMenu : PopupMenu {
	public event Action? RenameFile;
	public event Action? DeleteFile;
		
	public FilePopupMenu() {
		Name = nameof(FilePopupMenu);

		AddItem("Rename", 0);
		AddItem("Delete", 1);

		IdPressed += OnIdPressed;
	}

	protected void OnIdPressed(long id) {
		switch (id) {
			case 0: {
				RenameFile?.Invoke();
				break;
			}
			case 1: {
				DeleteFile?.Invoke();
				break;
			}
		}
	}
}