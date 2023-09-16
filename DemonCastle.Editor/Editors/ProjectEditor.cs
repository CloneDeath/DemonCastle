using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors; 

public partial class ProjectEditor : Control {
	public ProjectEditor(ProjectInfo projectInfo) {
		Name = $"Project - {projectInfo.FileName}";
		CustomMinimumSize = new Vector2I(300, 300);
	}
}