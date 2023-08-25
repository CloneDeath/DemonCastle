using DemonCastle.Editor.FileTreeView;
using DemonCastle.Editor.TopBar;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace {
		public EditorSpace(ProjectInfo project) {
			AddChild(new EditorTopBar {
				AnchorRight = 1,
				OffsetRight = 0,
				OffsetTop = 5,
				OffsetLeft = 5
			});
			AddChild(new EditorWorkspace(project) {
				AnchorRight = 1,
				AnchorBottom = 1,
				OffsetRight = 5,
				OffsetLeft = 5,
				OffsetBottom = -5,
				OffsetTop = 40
			});
		}
	}
}