using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors; 

public partial class ProjectEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.ProjectIcon;
	public override string TabText { get; }

	public ProjectEditor(ProjectInfo projectInfo) {
		Name = nameof(ProjectEditor);
		TabText = projectInfo.FileName;
		CustomMinimumSize = new Vector2I(300, 300);
	}
}