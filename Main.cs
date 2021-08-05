using System.Linq;
using Godot;

namespace DemonCastle {
    public class Main : Node2D
    {
        protected Button DownloadButton { get; }
        protected ProjectManager ProjectManager { get; } = new ProjectManager();

        public Main() {
            if (!ProjectManager.GetProjects().Any()) {
                ProjectManager.DownloadProjects();
            }
            AddChild(DownloadButton = new Button {
                Text = "Download"
            });
            DownloadButton.Connect("pressed", this, nameof(DownloadProjects));
        }

        protected void DownloadProjects() {
            ProjectManager.DownloadProjects();
            foreach (var project in ProjectManager.GetProjects()) {
                GD.Print(project.Name);
            }
        }
    }
}
