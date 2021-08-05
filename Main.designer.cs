using Godot;

namespace DemonCastle {
	public partial class Main {
		protected Button DownloadButton { get; }
		
		protected ProjectInfoList ProjectInfoList { get; }
		
		protected Button LaunchButton { get; }
		
		public Main() {
			AddChild(DownloadButton = new Button {
				Text = "Update Projects",
				RectPosition = new Vector2(10, 10)
			});
			DownloadButton.Connect("pressed", this, nameof(DownloadProjects));

			AddChild(ProjectInfoList = new ProjectInfoList {
				RectPosition = DownloadButton.RectPosition + new Vector2(0, 30),
				AnchorBottom = 1,
				AnchorRight = 1,
				RectSize = new Vector2(300, 300)
			});
			
			AddChild(LaunchButton = new Button {
				Text = "Launch",
				RectPosition = ProjectInfoList.RectPosition + new Vector2(0, 310)
			});
			LaunchButton.Connect("pressed", this, nameof(LaunchSelectedProject));
		}
	}
}