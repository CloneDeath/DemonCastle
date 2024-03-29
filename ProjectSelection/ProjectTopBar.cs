using System;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectSelection;

public partial class ProjectTopBar : HBoxContainer {
	public event Action? ReloadProjects;
	public event Action<ProjectResources, ProjectInfo>? ProjectEdit;

	protected Button NewProjectButton { get; }
	protected Button ImportButton { get; }

	protected FileDialog OpenFileDialog { get; }
	protected FileDialog OpenFolderDialog { get; }
	protected AcceptDialog ErrorPopup { get; }

	public ProjectTopBar() {
		Name = nameof(ProjectTopBar);

		AddChild(NewProjectButton = new Button { Text = "New Project" });
		NewProjectButton.Pressed += NewProjectButton_OnPressed;

		AddChild(ImportButton = new Button { Text = "Import Project" });
		ImportButton.Pressed += ImportButton_OnPressed;

		AddChild(OpenFolderDialog = new FileDialog {
			FileMode = FileDialog.FileModeEnum.OpenDir,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "New Project"
		});
		OpenFolderDialog.DirSelected += CreateProject;

		AddChild(OpenFileDialog = new FileDialog {
			Filters = new []{"*.dcp; Demon Castle Project"},
			FileMode = FileDialog.FileModeEnum.OpenFile,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "Import Project"
		});
		OpenFileDialog.FileSelected += ImportProject;

		AddChild(ErrorPopup = new AcceptDialog { Title = "Error" });
	}

	private void NewProjectButton_OnPressed() {
		OpenFolderDialog.Popup();
	}

	protected void ImportButton_OnPressed() {
		OpenFileDialog.Popup();
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

		ReloadProjects?.Invoke();
		ProjectEdit?.Invoke(projectWithResources.Resources, projectWithResources.Project);
	}


	protected void ImportProject(string filePath) {
		ProjectManager.ImportProject(filePath);
		ReloadProjects?.Invoke();
	}
}