using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle;

public partial class ProjectSelectionMenu {
	protected Button DownloadButton { get; }

	protected Button NewProjectButton { get; }
	protected Button ImportButton { get; }
	protected Button RemoveButton { get; }
	protected Button EditButton { get; }
	protected FileDialog OpenFileDialog { get; }
	protected FileDialog OpenFolderDialog { get; }
	protected AcceptDialog ErrorPopup { get; }

	protected InfoItemList<ProjectWithResources> ProjectList { get; }

	protected Button LaunchButton { get; }

	public ProjectSelectionMenu() {
		AddChild(DownloadButton = new Button {
			Text = "Update Projects",
			Position = new Vector2(10, 10)
		});
		DownloadButton.Pressed += this.DownloadProjects;


		AddChild(NewProjectButton = new Button {
			Text = "New Project",
			Position = DownloadButton.Position + new Vector2(310, 0)
		});
		NewProjectButton.Pressed += NewProjectButtonOnPressed;

		AddChild(ImportButton = new Button {
			Text = "Import Project",
			Position = NewProjectButton.Position + new Vector2(0, 40)
		});
		ImportButton.Pressed += OpenImportProject;

		AddChild(RemoveButton = new Button {
			Text = "Remove Project",
			Position = ImportButton.Position + new Vector2(0, 40)
		});
		RemoveButton.Pressed += this.RemoveProject;

		AddChild(EditButton = new Button {
			Text = "Edit Project",
			Position = RemoveButton.Position + new Vector2(0, 90)
		});
		EditButton.Pressed += this.EditProject;

		AddChild(OpenFolderDialog = new FileDialog {
			FileMode = FileDialog.FileModeEnum.OpenDir,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "New Project"
		});
		OpenFolderDialog.DirSelected += this.CreateProject;

		AddChild(OpenFileDialog = new FileDialog {
			Filters = new []{"*.dcp; Demon Castle Project"},
			FileMode = FileDialog.FileModeEnum.OpenFile,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "Import Project"
		});
		OpenFileDialog.FileSelected += this.ImportProject;

		AddChild((ErrorPopup = new AcceptDialog {
						 Title = "Error"
					 }));

		AddChild(ProjectList = new InfoItemList<ProjectWithResources> {
			Position = DownloadButton.Position + new Vector2(0, 40),
			AnchorBottom = 1,
			AnchorRight = 1
		});

		AddChild(LaunchButton = new Button {
			Text = "Launch",
			Position = ProjectList.Position + new Vector2(0, 310)
		});
		LaunchButton.Pressed += this.LaunchSelectedProject;
	}
}