using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Project;

public partial class ProjectEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.ProjectIcon;
	public override string TabText { get; }

	private ProjectDetails Details { get; }

	public ProjectEditor(ProjectInfo project) {
		Name = nameof(ProjectEditor);
		TabText = project.FileName;

		AddChild(Details = new ProjectDetails(project));
		Details.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}
}