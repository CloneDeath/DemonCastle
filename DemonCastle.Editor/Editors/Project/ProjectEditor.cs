using DemonCastle.Editor.Editors.Components.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Project;

public partial class ProjectEditor : Control {
	protected VBoxContainer Container { get; }
	protected ProjectDetails Details { get; }
	protected VariableCollectionEditor Variables { get; }

	public ProjectEditor(ProjectResources resources, ProjectInfo project) {
		Name = nameof(ProjectEditor);

		AddChild(Container = new VBoxContainer());
		Container.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		{
			Container.AddChild(Details = new ProjectDetails(project));
			Container.AddChild(Variables = new VariableCollectionEditor(resources));
			Variables.Load(project.Variables);
		}
	}
}