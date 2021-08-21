using System;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.ProjectFiles.Projects {
	public partial class ProjectSelectionMenu : Container {
		protected ProjectManager ProjectManager { get; } = new ProjectManager();

		public event Action<ProjectInfo> ProjectLoaded;

		public override void _Ready() {
			base._Ready();
            
			if (!ProjectManager.ProjectsExist) {
				ProjectManager.DownloadProjects();
			} else {
				ProjectList.Load(ProjectManager.GetProjects());
			}
		}

		public override void _Process(float delta) {
			base._Process(delta);
			LaunchButton.Disabled = !ProjectList.IsItemSelected;
			RemoveButton.Disabled = !(ProjectList.IsItemSelected && ProjectList.SelectedItem.IsImported);
		}

		protected void DownloadProjects() {
			ProjectManager.DownloadProjects();
			ProjectList.Load(ProjectManager.GetProjects());
		}

		protected void OpenImportProject() {
			OpenFileDialog.Popup_();
		}

		protected void ImportProject(string filePath) {
			ProjectManager.ImportProject(filePath);
			ProjectList.Load(ProjectManager.GetProjects());
		}

		protected void RemoveProject() {
			ProjectManager.RemoveProject(ProjectList.SelectedItem);
			ProjectList.Load(ProjectManager.GetProjects());
		}

		protected void LaunchSelectedProject() {
			var project = ProjectList.SelectedItem;
			ProjectLoaded?.Invoke(project);
		}
	}
}