using System;
using System.IO;
using System.Linq;
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
	protected MenuButton SampleButton { get; }

	protected FileDialog OpenFolderDialog { get; }
	protected FileDialog OpenFileDialog { get; }
	protected AcceptDialog ErrorPopup { get; }
	protected AcceptDialog InfoPopup { get; }

	public ProjectTopBar() {
		Name = nameof(ProjectTopBar);

		AddChild(NewProjectButton = new Button { Text = "New Project" });
		NewProjectButton.Pressed += NewProjectButton_OnPressed;

		AddChild(ImportButton = new Button { Text = "Import Project" });
		ImportButton.Pressed += ImportButton_OnPressed;

		AddChild(SampleButton = new MenuButton {
			Text = "New Project from Sample",
			Flat = false
		});
		foreach (var sampleProject in ProjectManager.SampleProjects) {
			SampleButton.GetPopup().AddItem(sampleProject.Name);
		}
		SampleButton.GetPopup().IdPressed += SampleButton_OnIdPressed;

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
		AddChild(InfoPopup = new AcceptDialog { Title = "Info" });
	}

	private void NewProjectButton_OnPressed() {
		OpenFolderDialog.Popup();
	}

	protected void ImportButton_OnPressed() {
		OpenFileDialog.Popup();
	}

	private void SampleButton_OnIdPressed(long id) {
		if (id >= ProjectManager.SampleProjects.Count) {
			throw new IndexOutOfRangeException();
		}

		var sample = ProjectManager.SampleProjects[(int)id];

		FileDialog sampleFolderDialog;
		AddChild(sampleFolderDialog = new FileDialog {
			FileMode = FileDialog.FileModeEnum.OpenDir,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "New Sample Project"
		});
		sampleFolderDialog.DirSelected += async dir => {
			try {
				if (Directory.EnumerateFiles(dir).Any() || Directory.EnumerateDirectories(dir).Any()) {
					throw new Exception("Folder must be empty.");
				}

				InfoPopup.DialogText = "Downloading Sample Project...";
				InfoPopup.Popup();
				var projectWithResources = await sample.DownloadProject(dir);
				InfoPopup.Hide();

				LocalProjectList.AddProject(projectWithResources.Project.FilePath);
				ReloadProjects?.Invoke();
				ProjectEdit?.Invoke(projectWithResources.Resources, projectWithResources.Project);
			}
			catch (Exception ex) {
				InfoPopup.Hide();
				ErrorPopup.DialogText = ex.Message;
				ErrorPopup.Popup();
			}
			finally {
				sampleFolderDialog.QueueFree();
			}
		};
		sampleFolderDialog.Popup();
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