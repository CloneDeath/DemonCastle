using System;
using System.Reflection;
using DemonCastle.Editor.Windows;
using DemonCastle.Editor.Windows.Character;
using DemonCastle.Editor.Windows.SpriteAtlas;
using DemonCastle.Editor.Windows.SpriteGrid;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using LevelWindow = DemonCastle.Editor.Windows.Level.LevelWindow;

namespace DemonCastle.Editor {
	public partial class WindowContainer : Control {
		protected Vector2I NextWindow = new(50, 50);
		
		public void ShowWindowFor(FileNavigator file) {
			if (WindowFileMap.TryGetValue(file, out var value)) {
				value.Show();
				return;
			}

			try {
				var window = GetWindow(file);
				WindowFileMap[file] = window;
				ShowWindow(window);
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

		public void ShowWindow(Window window) {
			AddChild(window);
			window.Position = (Vector2I)GlobalPosition + GetNextWindowLocation();
			window.Show();
		}

		protected Window GetWindow(FileNavigator file) {
			switch (file.Extension) {
				case ".dcp": return new ProjectWindow(file.ToProjectInfo());
				case ".dcc": return new CharacterWindow(file.ToCharacterInfo());
				case ".dcl": return new LevelWindow(file.ToLevelInfo());
				case ".dcsa": return new SpriteAtlasWindow(file.ToSpriteAtlasInfo());
				case ".dcsg": return new SpriteGridWindow(file.ToSpriteGridInfo());
				case ".txt": return new TextFileWindow(file.ToTextInfo());
				case ".png": return new ImageWindow(file);
				default: return new Window();
			}
		}
		
		protected Vector2I GetNextWindowLocation() {
			return NextWindow = (NextWindow + new Vector2I(50, 50)) % ((Vector2I)Size - new Vector2I(200, 200));
		}
	}
}