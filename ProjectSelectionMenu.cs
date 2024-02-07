using System;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle;

public partial class ProjectSelectionMenu : Container {
	protected ProjectManager ProjectManager { get; } = new();

	public event Action<ProjectResources, ProjectInfo>? ProjectLoaded;
	public event Action<ProjectResources, ProjectInfo>? ProjectEdit;

	public override void _Ready() {
		base._Ready();

		if (!ProjectManager.ProjectsExist) {
			ProjectManager.DownloadProjects().Wait();
		} else {
			ProjectList.Load(ProjectManager.GetProjects());
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		LaunchButton.Disabled = !ProjectList.IsItemSelected;
		RemoveButton.Disabled = !(ProjectList.IsItemSelected && ProjectList.SelectedItem.Project.IsImported);
		EditButton.Disabled = !(ProjectList.IsItemSelected && ProjectList.SelectedItem.Project.IsImported);
	}

	protected async void DownloadProjects() {
		await ProjectManager.DownloadProjects();
		ProjectList.Load(ProjectManager.GetProjects());
	}

	private void NewProjectButtonOnPressed() {
		OpenFolderDialog.Popup();
	}

	protected void CreateProject(string folderPath) {
		ProjectWithResources projectWithResources;
		try {
			projectWithResources = ProjectManager.CreateProject(folderPath);
		}
		catch (Exception ex) {
			ErrorPopup.DialogText = ex.Message;
			ErrorPopup.Popup();
			return;
		}

		ProjectList.Load(ProjectManager.GetProjects());
		ProjectEdit?.Invoke(projectWithResources.Resources, projectWithResources.Project);
	}

	protected void OpenImportProject() {
		OpenFileDialog.Popup();
	}

	protected void ImportProject(string filePath) {
		ProjectManager.ImportProject(filePath);
		ProjectList.Load(ProjectManager.GetProjects());
	}

	protected void RemoveProject() {
		ProjectManager.RemoveProject(ProjectList.SelectedItem.Project);
		ProjectList.Load(ProjectManager.GetProjects());
	}

	protected void EditProject() {
		var item = ProjectList.SelectedItem;
		ProjectEdit?.Invoke(item.Resources, item.Project);
	}

	protected void LaunchSelectedProject() {
		var item = ProjectList.SelectedItem;
		ProjectLoaded?.Invoke(item.Resources, item.Project);
	}
}