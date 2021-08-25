using DemonCastle.Editor.Windows;
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

			var window = GetWindow(file);
			WindowFileMap[file] = window;
			AddChild(window);
			window.RectPosition = RectGlobalPosition + GetNextWindowLocation();
			window.Show();
		}

		protected WindowDialog GetWindow(FileNavigator file) {
			switch (file.Extension) {
				case ".txt": return new TextFileWindow(file.ToTextFile());
				default: return new WindowDialog();
			}
		}
		
		protected Vector2 GetNextWindowLocation() {
			return NextWindow = (NextWindow + new Vector2(50, 50)).PosMod(RectSize - new Vector2(200, 200));
		}
	}
}