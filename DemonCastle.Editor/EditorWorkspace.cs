using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorWorkspace : Control {
		protected void FileTreeOnOnFileActivated(FileNavigator file) {
			WindowContainer.ShowWindowFor(file);
		}
	}
}