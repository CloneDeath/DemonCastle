using DemonCastle.Editor.FileTreeView;
using DemonCastle.Editor.TopBar;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor; 

public partial class EditorSpace {
	private EditorTopBar TopBar;
	protected ProjectInfo Project { get; }
	protected Window PlayWindow;
	
	public EditorSpace(ProjectInfo project) {
		Project = project;
		AddChild(PlayWindow = new Window {
			Visible = false,
			MinSize = new Vector2I(640, 480),
			Exclusive = true
		});
		PlayWindow.CloseRequested += PlayWindow.Hide;
		AddChild(TopBar = new EditorTopBar {
			AnchorRight = 1,
			OffsetRight = 0,
			OffsetTop = 5,
			OffsetLeft = 5
		});
		TopBar.PlayPressed += PlayPressed;
		AddChild(new EditorWorkspace(project) {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetRight = 5,
			OffsetLeft = 5,
			OffsetBottom = -5,
			OffsetTop = 40
		});
	}
}