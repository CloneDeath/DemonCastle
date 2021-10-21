using DemonCastle.Editor.FileTreeView;
using DemonCastle.Editor.TopBar;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace {
		public EditorSpace(ProjectInfo project) {
			AddChild(new EditorTopBar {
				AnchorRight = 1,
				MarginRight = 0,
				MarginBottom = 25
			});
			AddChild(new EditorWorkspace(project) {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginRight = 0,
				MarginBottom = 0,
				MarginTop = 25
			});
		}
	}
}