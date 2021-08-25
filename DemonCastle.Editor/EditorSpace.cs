using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace : CanvasLayer {
		protected void FileTreeOnOnItemActivated(FileNavigator file) {
			WindowContainer.ShowWindowFor(file);
			
		}
	}
}