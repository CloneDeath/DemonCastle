using System.IO.Compression;
using System.Net;
using Godot;

namespace DemonCastle {
    public class Main : Node2D
    {
        protected Button DownloadButton { get; }

        public Main() {
            AddChild(DownloadButton = new Button {
                Text = "Download"
            });
            DownloadButton.Connect("pressed", this, nameof(DownloadProjects));
        }

        protected void DownloadProjects() {
            var url = "https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip";
            var dest = System.IO.Path.GetTempFileName();
            using (var wc = new WebClient()) {
                wc.DownloadFile(url, dest);
            }

            var localPath = "user://Games/";
            var userLoc = ProjectSettings.GlobalizePath(localPath);
            System.IO.Directory.Delete(userLoc, true);
            ZipFile.ExtractToDirectory(dest, userLoc);
        }
    }
}
