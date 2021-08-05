using Godot;

namespace DemonCastle {
    public class Main : Node2D
    {
        protected Button DownloadButton { get; }
        protected ProjectManager ProjectManager { get; } = new ProjectManager();

        public Main() {
            AddChild(DownloadButton = new Button {
                Text = "Download"
            });
            DownloadButton.Connect("pressed", this, nameof(DownloadProjects));
        }

        public override void _Ready() {
            base._Ready();
            
            if (!ProjectManager.ProjectsExist) {
                ProjectManager.DownloadProjects();
            }
        }

        protected void DownloadProjects() {
            ProjectManager.DownloadProjects();
            foreach (var project in ProjectManager.GetProjects()) {
                GD.Print(project.Name);
            }
        }
    }
}
