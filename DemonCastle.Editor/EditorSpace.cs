using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace : CanvasLayer {
		protected void FileTreeOnOnItemActivated(FileNavigator file) {
			GD.Print("File opened " + file.FileName);
		}
	}
}