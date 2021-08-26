using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class ProjectWindow {
		public ProjectWindow(ProjectInfo spriteAtlasInfo) {
			WindowTitle = spriteAtlasInfo.FileName;
			RectSize = new Vector2(300, 300);
		}
	}
}