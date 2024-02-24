using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemonCastle.Game;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor;

public partial class EditArea : TabContainer {
	private readonly ProjectResources _resources;
	private readonly ProjectInfo _project;
	protected Dictionary<FileNavigator, Control> EditorFileMap { get; } = new();
	protected AcceptDialog ErrorWindow { get; }

	public EditArea(ProjectResources resources, ProjectInfo project) {
		_resources = resources;
		_project = project;
		Name = nameof(EditArea);
		DragToRearrangeEnabled = true;
		AddToGroup(nameof(EditArea));

		GetChild<TabBar>(0, true).TabCloseDisplayPolicy = TabBar.CloseButtonDisplayPolicy.ShowActiveOnly;
		GetChild<TabBar>(0, true).TabClosePressed += OnTabCloseButtonPressed;

		AddChild(ErrorWindow = new AcceptDialog {
			Exclusive = true
		}, false, InternalMode.Back);
	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if (@event.IsAction(InputActions.EditorClose, true)) {
			OnTabCloseButtonPressed(CurrentTab);
			AcceptEvent();
		} else if (@event.IsAction(InputActions.EditorSave, true)) {
			AcceptEvent();
		}
	}

	private FileNavigator? GetFileNavigator(int tab) {
		var control = GetTabControl(tab);
		if (control == null) return null;

		var mapItem = EditorFileMap.FirstOrDefault(t => t.Value == control);
		return mapItem.Key;
	}

	public void ShowEditorFor(FileNavigator file) {
		if (EditorFileMap.TryGetValue(file, out var value)) {
			CurrentTab = value.GetIndex();
			return;
		}

		try {
			var editorFileType = GetEditorFileType(file);
			var editor = editorFileType.GetEditor(_resources, _project, file);
			EditorFileMap[file] = editor;
			ShowEditor(file, editor, editorFileType);
		}
		catch (TargetInvocationException ex) {
			ErrorWindow.DialogText = $"Error: Could not open {file.FileName}.\nDetails: {ex.InnerException?.Message}";
			ErrorWindow.PopupCentered();
			throw;
		}
		catch (Exception ex) {
			ErrorWindow.DialogText = $"Error: Could not open {file.FileName}.\nDetails: {ex.Message}";
			ErrorWindow.PopupCentered();
			throw;
		}
	}

	public void ShowEditor(FileNavigator file, Control editor, IEditorFileType fileType) {
		AddChild(editor);
		var index = editor.GetIndex();
		SetTabIcon(index, fileType.Icon);
		SetTabTitle(index, file.FileName);
		CurrentTab = index;
	}

	protected virtual IEditorFileType GetEditorFileType(FileNavigator file) {
		return EditorFileType.All.FirstOrDefault(t => t.Extension == file.Extension) ??
		       throw new NotSupportedException($"No Editor for {file.Extension}");
	}

	private void OnTabCloseButtonPressed(long tab) {
		var control = GetTabControl((int)tab);
		if (control == null) return;

		var fileNavigator = GetFileNavigator((int)tab);
		if (fileNavigator != null) EditorFileMap.Remove(fileNavigator);
		control.QueueFree();
	}

	public void ReloadTabNames() {
		foreach (var pair in EditorFileMap) {
			var index = pair.Value.GetIndex();
			SetTabTitle(index, pair.Key.FileName);
		}
	}
}