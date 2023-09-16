using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.FileTreeView; 

public partial class DirectoryPopupMenu : PopupMenu {
	public event Action? AddDirectory;
	public event Action? CreateCharacterFile;
	public event Action? CreateSpriteAtlasFile;
	public event Action? CreateTextFile;
	public event Action? OpenFolder; 
	public event Action? RenameDirectory;
	public event Action? DeleteDirectory;
	
	private PopupAction[] Actions { get; }
	
	public DirectoryPopupMenu() {
		Name = nameof(DirectoryPopupMenu);
		
		Actions = new[] {
			new PopupAction {
				Text = "Add Directory",
				Icon = IconTextures.FolderIcon,
				Action = () => AddDirectory?.Invoke()
			},
			new PopupAction {
				Text = "Create Character File",
				Icon = IconTextures.CharacterIcon,
				Action = () => CreateCharacterFile?.Invoke()
			},
			new PopupAction {
				Text = "Create Sprite Atlas File",
				Icon = IconTextures.SpriteAtlasIcon,
				Action = () => CreateSpriteAtlasFile?.Invoke()
			},
			new PopupAction {
				Text = "Create Text File",
				Icon = IconTextures.TextFileIcon,
				Action = () => CreateTextFile?.Invoke()
			},
			new PopupAction {
				Text = "Open Folder...",
				Action = () => OpenFolder?.Invoke()
			},
			new PopupAction {
				Text = "Rename...",
				Action = () => RenameDirectory?.Invoke()
			},
			new PopupAction {
				Text = "Delete...",
				Action = () => DeleteDirectory?.Invoke()
			}
		};

		for (var i = 0; i < Actions.Length; i++) {
			var action = Actions[i];
			AddItem(action.Text, i);
			if (action.Icon != null) SetItemIcon(i, action.Icon);
		}

		IdPressed += OnIdPressed;
	}

	protected void OnIdPressed(long id) {
		if (id > Actions.Length) {
			throw new NotSupportedException($"Id {id} not handled in DirectoryPopupMenu.");
		}

		var action = Actions[id];
		action.Action();
	}
}