using System;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectSelection;

public partial class ProjectSelectionMenu : VBoxContainer {
	protected ProjectManager ProjectManager { get; } = new();

	public event Action<ProjectResources, ProjectInfo>? ProjectRun;
	public event Action<ProjectResources, ProjectInfo>? ProjectEdit;

	protected ProjectTopBar ProjectTopBar { get; }
	protected ProjectItemList ProjectList { get; }
	protected ProjectActions ProjectActions { get; }

	public ProjectSelectionMenu() {
		Name = nameof(ProjectSelectionMenu);

		AddChild(ProjectTopBar = new ProjectTopBar());
		ProjectTopBar.ReloadProjects += () => {
			ProjectList?.Load(ProjectManager.GetProjects());
		};
		ProjectTopBar.ProjectEdit += (resources, info) => {
			ProjectEdit?.Invoke(resources, info);
		};

		HBoxContainer bottomArea;
		AddChild(bottomArea = new HBoxContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		{
			bottomArea.AddChild(ProjectList = new ProjectItemList {
				SizeFlagsHorizontal = SizeFlags.ExpandFill
			});
			bottomArea.AddChild(ProjectActions = new ProjectActions());
			ProjectActions.EditPressed += EditSelectedProject;
			ProjectActions.RunPressed += RunSelectedProject;
			ProjectActions.RemovePressed += RemoveSelectedProject;
		}
	}

	public override void _Ready() {
		base._Ready();
		ProjectList.Load(ProjectManager.GetProjects());
	}

	public override void _Process(double delta) {
		base._Process(delta);
		ProjectActions.Disabled = !ProjectList.IsItemSelected;
	}

	protected void EditSelectedProject() {
		var item = ProjectList.SelectedItem;
		ProjectEdit?.Invoke(item.Resources, item.Project);
	}

	protected void RunSelectedProject() {
		var item = ProjectList.SelectedItem;
		ProjectRun?.Invoke(item.Resources, item.Project);
	}

	protected void RemoveSelectedProject() {
		ProjectManager.RemoveProject(ProjectList.SelectedItem.Project);
		ProjectList.Load(ProjectManager.GetProjects());
	}
}