using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor; 

public partial class EditorWorkspace : Control {
	protected void ExplorerOnFileActivated(FileNavigator file) {
		EditArea.ShowEditorFor(file);
	}
}