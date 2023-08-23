using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class ProjectWindow {
		public ProjectWindow(ProjectInfo projectInfo) {
			WindowTitle = $"Project - {projectInfo.FileName}";
			Size = new Vector2(300, 300);
			CustomMinimumSize = Size;
		}
	}
}