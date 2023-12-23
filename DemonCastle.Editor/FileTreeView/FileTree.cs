using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Icons;
using DemonCastle.Game;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class FileTree : Tree {
	public event Action<FileNavigator>? OnFileActivated;

	protected Dictionary<TreeItem, FileNavigator> FileMap { get; } = new();
	protected Dictionary<TreeItem, DirectoryNavigator> DirectoryMap { get; } = new();

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);
		if (Input.IsActionJustPressed(InputActions.EditorRename)) {
			OnRename();
		}
	}

	protected void FileActivated() {
		var selected = GetSelected();
		if (selected == null || !FileMap.ContainsKey(selected)) return;

		OnFileActivated?.Invoke(FileMap[selected]);
	}

	protected void OnItemSelected(Vector2 position, long button) {
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

	public void Refresh() => CreateTree();

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

	protected static Texture2D GetIconForFile(string extension) {
		return EditorFileType.All.FirstOrDefault(e => e.Extension == extension)?.Icon
			   ?? IconTextures.UnknownIcon;
	}

	public void OnAddDirectorySelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateDirectory("directory");
		CreateTree();
	}

	public void OnCreateEditorFileSelected(IEditorFileType fileType) {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];

		dirNav.CreateFile(fileType.Name, fileType.Extension, fileType.CreateFileInstance());

		CreateTree();
	}


	public void OnCreateTextFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateEmptyFile("text", FileType.Text.Extension);

		CreateTree();
	}

	protected FileNavigator? SelectedFile {
		get {
			var selected = GetSelected();
			return FileMap.TryGetValue(selected, out var value) ? value : null;
		}
	}

	protected DirectoryNavigator? SelectedDirectory {
		get {
			var selected = GetSelected();
			return DirectoryMap.TryGetValue(selected, out var value) ? value : null;
		}
	}

	protected void OnOpenFolder() {
		var folder = SelectedDirectory?.Directory ?? throw new Exception("No Directory Selected");
		OS.ShellShowInFileManager(folder);
	}

	protected void OnRename() {
		if (SelectedFile != null) {
			ConfirmRename.Text = SelectedFile.FileName;
			ConfirmRename.Select(0, SelectedFile.FileNameWithoutExtension.Length);
		} else if (SelectedDirectory != null) {
			ConfirmRename.Text = SelectedDirectory.DirectoryName;
			ConfirmRename.SelectAll();
		} else {
			return;
		}
		ConfirmRename.PopupCentered();
		ConfirmRename.FocusLineEdit();
	}

	protected void OnRenameConfirmed() {
		SelectedFile?.RenameFile($"{ConfirmRename.Text}");
		SelectedDirectory?.RenameDirectory($"{ConfirmRename.Text}");
		CreateTree();
	}

	protected void OnDelete() {
		ConfirmDelete.PopupCentered();
	}

	protected void OnDeleteConfirmed() {
		SelectedFile?.DeleteFile();
		SelectedDirectory?.DeleteDirectory();

		CreateTree();
	}

	public override Variant _GetDragData(Vector2 atPosition) {
		DropModeFlags = (int) DropModeFlagsEnum.OnItem;

		var item = GetItemAtPosition(atPosition);

		var preview = new HBoxContainer();
		preview.AddChild(new TextureRect {
			Texture = item.GetIcon(0),
			StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered
		});
		preview.AddChild(new Label {
			Text = item.GetText(0)
		});
		SetDragPreview(preview);

		return item;
	}

	public override bool _CanDropData(Vector2 position, Variant data) {
		var target = GetItemAtPosition(position);
		return target != null && target != (TreeItem)data && DirectoryMap.ContainsKey(target);
	}

	public override void _DropData(Vector2 position, Variant data) {
		var target = GetItemAtPosition(position);
		var shift = GetDropSectionAtPosition(position);
		var treeItem = (TreeItem)data;

		var dir = DirectoryMap[target];
		var file = FileMap[treeItem];
		file.MoveTo(dir);

		switch (shift) {
			case -1:
				treeItem.MoveBefore(target);
				break;
			case 0:
				treeItem.GetParent().RemoveChild(treeItem);
				target.AddChild(treeItem);
				break;
			case 1:
				treeItem.MoveAfter(target);
				break;
		}
	}
}