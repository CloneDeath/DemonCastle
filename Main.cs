using DemonCastle.Projects;
using Godot;

namespace DemonCastle {
    public partial class Main : Node2D
    {
        protected ProjectManager ProjectManager { get; } = new ProjectManager();

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
            GD.Print(project.Name);
        }
    }
}
