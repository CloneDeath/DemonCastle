using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Character;
using DemonCastle.Editor.Editors.Level;
using DemonCastle.Editor.Editors.SpriteAtlas;
using DemonCastle.Editor.Editors.SpriteGrid;
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
		}
		catch (Exception ex) {
			ErrorWindow.DialogText = $"Error: Could not open {file.FileName}.\nDetails: {ex.Message}";
			ErrorWindow.PopupCentered();
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
		return file.Extension switch {
			".dcp" => new ProjectEditor(file.ToProjectInfo()),
			".dcc" => new CharacterEditor(file.ToCharacterInfo()),
			".dcl" => new LevelEditor(file.ToLevelInfo()),
			".dcsa" => new SpriteAtlasEditor(file.ToSpriteAtlasInfo()),
			".dcsg" => new SpriteGridEditor(file.ToSpriteGridInfo()),
			".txt" => new TextFileEditor(file.ToTextInfo()),
			".png" => new ImageEditor(file),
			_ => throw new NotSupportedException($"No Editor for {file.Extension}")
		};
	}

	private void OnTabButtonPressed(long tab) {
		var control = GetTabControl((int)tab);
		var mapItem = EditorFileMap.FirstOrDefault(t => t.Value == control);
		if (mapItem.Key != null) EditorFileMap.Remove(mapItem.Key);
		control.QueueFree();
	}
}