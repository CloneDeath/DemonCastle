using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class ProjectWindow {
		public ProjectWindow(ProjectInfo projectInfo) {
			Title = $"Project - {projectInfo.FileName}";
			Size = new Vector2I(300, 300);
			MinSize = Size;
		}
	}
}