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

        protected void DownloadProjects() {
            ProjectManager.DownloadProjects();
            ProjectInfoList.Load(ProjectManager.GetProjects());
        }
    }
}
