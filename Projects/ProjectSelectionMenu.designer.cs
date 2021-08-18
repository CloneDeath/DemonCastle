using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Projects {
	public partial class ProjectSelectionMenu {
		protected Button DownloadButton { get; }
		protected Button ImportButton { get; }
		protected Button RemoveButton { get; }
		protected FileDialog OpenFileDialog { get; }
		
		protected InfoItemList<ProjectInfo> ProjectList { get; }
		
		protected Button LaunchButton { get; }
		
		public ProjectSelectionMenu() {
			AddChild(DownloadButton = new Button {
				Text = "Update Projects",
				RectPosition = new Vector2(10, 10)
			});
			DownloadButton.Connect("pressed", this, nameof(DownloadProjects));
			
			AddChild(ImportButton = new Button {
				Text = "Import Project",
				RectPosition = DownloadButton.RectPosition + new Vector2(310, 0)
			});
			ImportButton.Connect("pressed", this, nameof(OpenImportProject));
			
			AddChild(RemoveButton = new Button {
				Text = "Remove Project",
				RectPosition = ImportButton.RectPosition + new Vector2(0, 30)
			});
			RemoveButton.Connect("pressed", this, nameof(RemoveProject));
			
			AddChild(OpenFileDialog = new FileDialog {
				Filters = new []{"*.dcp; Demon Castle Project"},
				Mode = FileDialog.ModeEnum.OpenFile,
				PopupExclusive = true,
				Access = FileDialog.AccessEnum.Filesystem,
				RectSize = new Vector2(800, 600),
				Resizable = true,
				WindowTitle = "Import Project"
			});
			OpenFileDialog.Connect("file_selected", this, nameof(ImportProject));

			AddChild(ProjectList = new InfoItemList<ProjectInfo> {
				RectPosition = DownloadButton.RectPosition + new Vector2(0, 30),
				AnchorBottom = 1,
				AnchorRight = 1
			});
			
			AddChild(LaunchButton = new Button {
				Text = "Launch",
				RectPosition = ProjectList.RectPosition + new Vector2(0, 310)
			});
			LaunchButton.Connect("pressed", this, nameof(LaunchSelectedProject));
		}
	}
}