using Godot;

namespace DemonCastle {
	public partial class Main {
		protected Button DownloadButton { get; }
		
		protected ProjectInfoList ProjectInfoList { get; }
		
		public Main() {
			AddChild(DownloadButton = new Button {
				Text = "Update Projects"
			});
			DownloadButton.Connect("pressed", this, nameof(DownloadProjects));

			AddChild(ProjectInfoList = new ProjectInfoList {
				RectPosition = new Vector2(0, 20),
				AnchorBottom = 1,
				AnchorRight = 1,
				RectSize = new Vector2(300, 300)
			});
		}
	}
}