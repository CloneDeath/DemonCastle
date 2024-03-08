using System;
using System.Linq;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.FileTreeView.Popups;

public partial class DirectoryPopupMenu : PopupMenu {
	public event Action? AddDirectory;

	public event Action<IEditorFileType>? CreateEditorFile;

	public event Action? CreateTextFile;
	public event Action? OpenFolderInExplorer;
	public event Action? OpenFolderInTerminal;
	public event Action? OpenFolderInVisualStudioCode;
	public event Action? RenameDirectory;
	public event Action? DeleteDirectory;

	private PopupAction[] Actions { get; }

	public DirectoryPopupMenu() {
		Name = nameof(DirectoryPopupMenu);

		var createFileActions = EditorFileType.CreatableFileTypes
			.Select(fileType => new PopupAction {
				Text = $"Create {fileType.Name} File",
				Icon = fileType.Icon,
				Action = () => CreateEditorFile?.Invoke(fileType)
			})
			.ToArray();
		Actions = new[] {
			new PopupAction {
				Text = "Add Directory",
				Icon = IconTextures.FolderIcon,
				Action = () => AddDirectory?.Invoke()
			}
		}.Concat(createFileActions)
		 .Concat(new[]{
			 new PopupAction {
				 Text = "Create Text File",
				 Icon = IconTextures.File.TextFileIcon,
				 Action = () => CreateTextFile?.Invoke()
			 },
			 new PopupAction {
				 Text = "Open In",
				 Children = new[] {
					 new PopupAction {
						 Text = "Explorer",
						 Icon = IconTextures.OpenFolder.InExplorerIcon,
						 Action = () => OpenFolderInExplorer?.Invoke()
					 },
					 new PopupAction {
						 Text = "Terminal",
						 Icon = IconTextures.OpenFolder.InTerminalIcon,
						 Action = () => OpenFolderInTerminal?.Invoke()
					 },
					 new PopupAction {
						 Text = "Visual Studio Code",
						 Icon = IconTextures.OpenFolder.InVisualStudioCodeIcon,
						 Action = () => OpenFolderInVisualStudioCode?.Invoke()
					 }
				 }
			 },
			 new PopupAction {
				 Text = "Rename...",
				 Action = () => RenameDirectory?.Invoke()
			 },
			 new PopupAction {
				 Text = "Delete...",
				 Action = () => DeleteDirectory?.Invoke()
			 }
		 }).ToArray();

        AddPopupActionsToMenu(this, Actions);
	}

	protected static void AddPopupActionsToMenu(PopupMenu target, PopupAction[] actions) {
		for (var i = 0; i < actions.Length; i++) {
			var action = actions[i];

			if (!action.Children.Any()) target.AddItem(action.Text, i);
			else {
				PopupMenu child;
				target.AddChild(child = new PopupMenu {
					Name = $"{action.Text}_Submenu ({Guid.NewGuid()})"
				});
				target.AddSubmenuItem(action.Text, child.Name, i);
				AddPopupActionsToMenu(child, action.Children);
			}
			if (action.Icon != null) target.SetItemIcon(i, action.Icon);
		}

		target.IdPressed += id => OnIdPressed(actions, id);
	}

	protected static void OnIdPressed(PopupAction[] actions, long id) {
		if (id > actions.Length) {
			throw new NotSupportedException($"Id {id} not handled in DirectoryPopupMenu.");
		}

		var action = actions[id];
		action.Action();
	}
}