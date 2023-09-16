using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemonCastle.Editor.Windows;
using DemonCastle.Editor.Windows.Character;
using DemonCastle.Editor.Windows.Level;
using DemonCastle.Editor.Windows.SpriteAtlas;
using DemonCastle.Editor.Windows.SpriteGrid;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using CharacterEditor = DemonCastle.Editor.Editors.Character.CharacterEditor;
using ImageEditor = DemonCastle.Editor.Editors.ImageEditor;
using LevelEditor = DemonCastle.Editor.Editors.Level.LevelEditor;
using ProjectEditor = DemonCastle.Editor.Editors.ProjectEditor;
using SpriteAtlasEditor = DemonCastle.Editor.Editors.SpriteAtlas.SpriteAtlasEditor;
using SpriteGridEditor = DemonCastle.Editor.Editors.SpriteGrid.SpriteGridEditor;
using TextFileEditor = DemonCastle.Editor.Editors.TextFileEditor;

namespace DemonCastle.Editor; 

public partial class EditArea : TabContainer {
	protected Dictionary<FileNavigator, Control> EditorFileMap { get; } = new();

	public void ShowWindowFor(FileNavigator file) {
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

	public void ShowEditor(Control editor) {
		AddChild(editor);
		CurrentTab = editor.GetIndex();
	}

	protected virtual Control GetEditor(FileNavigator file) {
		return file.Extension switch {
			".dcp" => new ProjectEditor(file.ToProjectInfo()),
			".dcc" => new CharacterEditor(file.ToCharacterInfo()),
			".dcl" => new LevelEditor(file.ToLevelInfo()),
			".dcsa" => new SpriteAtlasEditor(file.ToSpriteAtlasInfo()),
			".dcsg" => new SpriteGridEditor(file.ToSpriteGridInfo()),
			".txt" => new TextFileEditor(file.ToTextInfo()),
			".png" => new ImageEditor(file),
			_ => new Control()
		};
	}

	private void OnTabButtonPressed(long tab) {
		var control = GetTabControl((int)tab);
		var mapItem = EditorFileMap.FirstOrDefault(t => t.Value == control);
		if (mapItem.Key != null) EditorFileMap.Remove(mapItem.Key);
		control.QueueFree();
	}
}