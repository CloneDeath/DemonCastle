using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemonCastle.Editor.Editors;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor;

public partial class EditArea : TabContainer {
	protected Dictionary<FileNavigator, BaseEditor> EditorFileMap { get; } = new();

	public void ShowEditorFor(FileNavigator file) {
		if (EditorFileMap.TryGetValue(file, out var value)) {
			CurrentTab = value.GetIndex();
			return;
		}

		try {
			var editor = GetEditor(file);
			EditorFileMap[file] = editor;
			ShowEditor(editor);
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

	public void ShowEditor(BaseEditor editor) {
		AddChild(editor);
		var index = editor.GetIndex();
		SetTabIcon(index, editor.TabIcon);
		SetTabTitle(index, editor.TabText);
		CurrentTab = index;
	}

	protected virtual BaseEditor GetEditor(FileNavigator file) {
		return EditorFileType.All.FirstOrDefault(t => t.Extension == file.Extension)?.GetEditor(file) ??
		       throw new NotSupportedException($"No Editor for {file.Extension}");
	}

	private void OnTabButtonPressed(long tab) {
		var control = GetTabControl((int)tab);
		var mapItem = EditorFileMap.FirstOrDefault(t => t.Value == control);
		if (mapItem.Key != null) EditorFileMap.Remove(mapItem.Key);
		control.QueueFree();
	}
}