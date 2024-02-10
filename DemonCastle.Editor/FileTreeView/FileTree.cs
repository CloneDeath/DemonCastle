using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.FileInfo;
using DemonCastle.Editor.FileTreeView.Popups;
using DemonCastle.Editor.Icons;
using DemonCastle.Game;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class FileTree : Tree {
	private readonly ProjectResources _resources;
	private readonly ProjectPreferencesInfo _preferences;

	protected DirectoryPopupMenu DirectoryPopupMenu { get; }
	protected FilePopupMenu FilePopupMenu { get; }
	protected DeleteDialog ConfirmDelete { get; }
	protected RenameDialog ConfirmRename { get; }
	protected GetNameDialog GetNameDialog { get; }

	public event Action<FileNavigator>? OnFileActivated;
	public event Action? TreeReloaded;

	protected Dictionary<TreeItem, FileNavigator> FileMap { get; } = new();
	protected Dictionary<TreeItem, DirectoryNavigator> DirectoryMap { get; } = new();

	public FileTree(ProjectResources resources, ProjectPreferencesInfo preferences) {
		_resources = resources;
		_preferences = preferences;
		Name = nameof(FileTree);
		AllowRmbSelect = true;

		AddChild(GetNameDialog = new GetNameDialog());

		AddChild(ConfirmRename = new RenameDialog());
		ConfirmRename.Confirmed += OnRenameConfirmed;

		AddChild(ConfirmDelete = new DeleteDialog());
		ConfirmDelete.Confirmed += OnDeleteConfirmed;

		AddChild(DirectoryPopupMenu = new DirectoryPopupMenu());
		DirectoryPopupMenu.AddDirectory += OnAddDirectorySelected;
		DirectoryPopupMenu.CreateEditorFile += OnCreateEditorFileSelected;
		DirectoryPopupMenu.CreateTextFile += OnCreateTextFileSelected;
		DirectoryPopupMenu.OpenFolder += OnOpenFolder;
		DirectoryPopupMenu.RenameDirectory += OnRename;
		DirectoryPopupMenu.DeleteDirectory += OnDelete;

		AddChild(FilePopupMenu = new FilePopupMenu());
		FilePopupMenu.RenameFile += OnRename;
		FilePopupMenu.DeleteFile += OnDelete;

		ReloadTree();
		ItemActivated += OnItemActivated;
		ItemMouseSelected += OnItemSelected;

		ItemCollapsed += OnItemCollapsed;
	}

	private void OnItemCollapsed(TreeItem item) {
		if (!DirectoryMap.TryGetValue(item, out var dir)) return;
		if (item.Collapsed) {
			_preferences.ExpandedDirectories.Remove(dir.Directory);
		} else {
			_preferences.ExpandedDirectories.Add(dir.Directory);
		}
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);
		if (Input.IsActionJustPressed(InputActions.EditorRename)) {
			OnRename();
		}
	}

	protected void OnItemActivated() {
		var selected = GetSelected();
		if (selected == null) return;
		if (FileMap.TryGetValue(selected, out var value)) {
			OnFileActivated?.Invoke(value);
		} else if (DirectoryMap.TryGetValue(selected, out var dir)) {
			selected.Collapsed = !selected.Collapsed;
			if (selected.Collapsed) {
				_preferences.ExpandedDirectories.Remove(dir.Directory);
			} else {
				_preferences.ExpandedDirectories.Add(dir.Directory);
			}
		}
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

	public void Refresh() => ReloadTree();

	public void Collapse() {
		var root = GetRoot();
		root.Collapsed = false;
		_preferences.ExpandedDirectories.Clear();

		foreach (var child in root.GetChildren()) {
			child.SetCollapsedRecursive(true);
		}
	}

	public void Expand() {
		var root = GetRoot();
		root.Collapsed = false;

		foreach (var child in root.GetChildren()) {
			child.SetCollapsedRecursive(false);
		}

		_preferences.ExpandedDirectories.Clear();
		var directories = DirectoryMap.Values.Select(d => d.Directory);
		_preferences.ExpandedDirectories.AddRange(directories);
	}

	protected void ReloadTree() {
		Clear();
		FileMap.Clear();
		DirectoryMap.Clear();
		CreateDirectory(null, _resources.GetRoot());
		TreeReloaded?.Invoke();
	}

	protected void CreateDirectory(TreeItem? parent, DirectoryNavigator directory) {
		if (directory.DirectoryName.StartsWith(".")) return;

		var dir = CreateItem(parent);
		dir.SetText(0, directory.DirectoryName);
		dir.SetIcon(0, IconTextures.FolderIcon);
		DirectoryMap[dir] = directory;
		if (parent == null || _preferences.ExpandedDirectories.Contains(directory.Directory)) {
			dir.Collapsed = false;
		} else {
			dir.Collapsed = true;
		}

		foreach (var subDirectory in directory.GetDirectories()) {
			CreateDirectory(dir, _resources.GetDirectory(subDirectory));
		}

		foreach (var file in directory.GetFiles()) {
			CreateFile(dir, _resources.GetFile(file));
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
			   ?? IconTextures.File.UnknownIcon;
	}

	public async void OnAddDirectorySelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		GetNameDialog.Title = "New Directory";
		var name = await GetNameDialog.GetName();
		if (name == null) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateDirectory(name);
		ReloadTree();
	}

	public async void OnCreateEditorFileSelected(IEditorFileType fileType) {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;

		GetNameDialog.Title = $"New {fileType.Name}";
		var name = await GetNameDialog.GetName();
		if (name == null) return;
		if (name.EndsWith(fileType.Extension)) {
			name = name[..^fileType.Extension.Length];
		}
		var dirNav = DirectoryMap[selected];
		var data = fileType.CreateFileInstance(name);
		var contents = Serializer.Serialize(data);
		dirNav.CreateFile(name, fileType.Extension, contents);

		ReloadTree();
	}


	public async void OnCreateTextFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;

		GetNameDialog.Title = "New Text File";
		var name = await GetNameDialog.GetName();
		if (name == null) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateEmptyFile(name, FileType.Text.Extension);

		ReloadTree();
	}

	protected FileNavigator? SelectedFile {
		get {
			var selected = GetSelected();
			return FileMap.GetValueOrDefault(selected);
		}
	}

	protected DirectoryNavigator? SelectedDirectory {
		get {
			var selected = GetSelected();
			return DirectoryMap.GetValueOrDefault(selected);
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
		ReloadTree();
	}

	protected void OnDelete() {
		ConfirmDelete.PopupCentered();
	}

	protected void OnDeleteConfirmed() {
		SelectedFile?.DeleteFile();
		SelectedDirectory?.DeleteDirectory();

		ReloadTree();
	}

	#region Drag-and-drop
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
	#endregion
}