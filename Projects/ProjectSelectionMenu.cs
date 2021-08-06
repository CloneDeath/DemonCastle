using System;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Projects {
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
		}

		protected void DownloadProjects() {
			ProjectManager.DownloadProjects();
			ProjectList.Load(ProjectManager.GetProjects());
		}

		protected void LaunchSelectedProject() {
			var project = ProjectList.SelectedItem;
			ProjectLoaded?.Invoke(project);
		}
	}
}