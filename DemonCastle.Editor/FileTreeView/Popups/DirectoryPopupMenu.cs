using System;
using System.Linq;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.FileTreeView.Popups;

public partial class DirectoryPopupMenu : PopupMenu {
	public event Action? AddDirectory;

	public event Action<IEditorFileType>? CreateEditorFile;

	public event Action? CreateTextFile;
	public event Action? OpenFolder;
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
		 }).ToArray();


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