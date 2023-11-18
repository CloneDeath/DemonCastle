using System;
using System.Collections.Generic;
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
		if (!FileMap.ContainsKey(selected)) return;

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
		return extension switch {
			".dcp" => IconTextures.ProjectIcon,
			".dcl" => IconTextures.LevelIcon,
			".dcsa" => IconTextures.SpriteAtlasIcon,
			".dcsg" => IconTextures.SpriteGridIcon,
			".dcc" => IconTextures.CharacterIcon,
			".txt" => IconTextures.TextFileIcon,
			".png" => IconTextures.TextureIcon,
			_ => IconTextures.UnknownIcon
		};
	}

	public void OnAddDirectorySelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateDirectory("directory");
		CreateTree();
	}

	public void OnCreateSpriteAtlasFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateFile("sprite-atlas", "dcsa", new SpriteAtlasFile());

		CreateTree();
	}

	public void OnCreateSpriteGridFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateFile("sprite-grid", "dcsg", new SpriteGridFile());

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

	public void OnCreateWeaponFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateFile("weapon", "dcw", new WeaponFile {
			Name = "weapon"
		});

		CreateTree();
	}

	public void OnCreateLevelFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateFile("level", "dcl", new LevelFile {
			Name = "level"
		});

		CreateTree();
	}

	public void OnCreateTextFileSelected() {
		var selected = GetSelected();
		if (!DirectoryMap.ContainsKey(selected)) return;
		var dirNav = DirectoryMap[selected];
		dirNav.CreateEmptyFile("text", "txt");

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
		ConfirmRename.Text = SelectedFile?.FileNameWithoutExtension
							 ?? SelectedDirectory?.DirectoryName
							 ?? string.Empty;
		ConfirmRename.PopupCentered();
		ConfirmRename.FocusLineEdit();
	}

	protected void OnRenameConfirmed() {
		SelectedFile?.RenameFile($"{ConfirmRename.Text}{SelectedFile.Extension}");
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
}