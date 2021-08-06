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
				ProjectInfoList.Load(ProjectManager.GetProjects());
			}
		}

		public override void _Process(float delta) {
			base._Process(delta);
			LaunchButton.Disabled = !ProjectInfoList.IsProjectSelected;
		}

		protected void DownloadProjects() {
			ProjectManager.DownloadProjects();
			ProjectInfoList.Load(ProjectManager.GetProjects());
		}

		protected void LaunchSelectedProject() {
			var project = ProjectInfoList.SelectedProject;
			ProjectLoaded?.Invoke(project);
		}
	}
}