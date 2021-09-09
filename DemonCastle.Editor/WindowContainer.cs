using System;
using System.Reflection;
using DemonCastle.Editor.Windows;
using DemonCastle.Editor.Windows.SpriteAtlas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class WindowContainer : Control {
		protected Vector2 NextWindow = new Vector2(50, 50);
		
		public void ShowWindowFor(FileNavigator file) {
			if (WindowFileMap.ContainsKey(file)) {
				WindowFileMap[file].Show();
				return;
			}

			try {
				var window = GetWindow(file);
				WindowFileMap[file] = window;
				AddChild(window);
				window.RectPosition = RectGlobalPosition + GetNextWindowLocation();
				window.Show();
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

		protected WindowDialog GetWindow(FileNavigator file) {
			switch (file.Extension) {
				case ".dcp": return new ProjectWindow(file.ToProjectInfo());
				case ".dcc": return new CharacterWindow(file.ToCharacterInfo());
				case ".dcl": return new LevelWindow(file.ToLevelInfo());
				case ".dcsa": return new SpriteAtlasWindow(file.ToSpriteAtlasInfo());
				case ".dcsg": return new SpriteGridWindow(file.ToSpriteGridInfo());
				case ".txt": return new TextFileWindow(file.ToTextInfo());
				case ".png": return new ImageWindow(file);
				default: return new WindowDialog();
			}
		}
		
		protected Vector2 GetNextWindowLocation() {
			return NextWindow = (NextWindow + new Vector2(50, 50)).PosMod(RectSize - new Vector2(200, 200));
		}
	}
}