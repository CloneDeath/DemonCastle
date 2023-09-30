using System;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle;

public partial class ProjectSelectionMenu : Container {
	protected ProjectManager ProjectManager { get; } = new();

	public event Action<ProjectInfo>? ProjectLoaded;
	public event Action<ProjectInfo>? ProjectEdit;

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
		RemoveButton.Disabled = !(ProjectList.IsItemSelected && ProjectList.SelectedItem.IsImported);
		EditButton.Disabled = !(ProjectList.IsItemSelected && ProjectList.SelectedItem.IsImported);
	}

	protected async void DownloadProjects() {
		await ProjectManager.DownloadProjects();
		ProjectList.Load(ProjectManager.GetProjects());
	}

	private void NewProjectButtonOnPressed() {
		OpenFolderDialog.Popup();
	}

	protected void CreateProject(string folderPath) {
		ProjectInfo project;
		try {
			project = ProjectManager.CreateProject(folderPath);
		}
		catch (Exception ex) {
			ErrorPopup.DialogText = ex.Message;
			ErrorPopup.Popup();
			return;
		}

		ProjectList.Load(ProjectManager.GetProjects());
		ProjectEdit?.Invoke(project);
	}

	protected void OpenImportProject() {
		OpenFileDialog.Popup();
	}

	protected void ImportProject(string filePath) {
		ProjectManager.ImportProject(filePath);
		ProjectList.Load(ProjectManager.GetProjects());
	}

	protected void RemoveProject() {
		ProjectManager.RemoveProject(ProjectList.SelectedItem);
		ProjectList.Load(ProjectManager.GetProjects());
	}

	protected void EditProject() {
		var project = ProjectList.SelectedItem;
		ProjectEdit?.Invoke(project);
	}

	protected void LaunchSelectedProject() {
		var project = ProjectList.SelectedItem;
		ProjectLoaded?.Invoke(project);
	}
}