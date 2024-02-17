using DemonCastle.Editor.FileInfo;
using DemonCastle.Editor.FileTreeView;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorWorkspace : HSplitContainer {
	private readonly ProjectPreferencesInfo _preferences;
	protected ExplorerPanel Explorer { get; }
	protected EditArea EditArea { get; }

	public EditorWorkspace(ProjectResources resources, ProjectInfo project, ProjectPreferencesInfo preferences) {
		_preferences = preferences;
		Name = nameof(EditorWorkspace);

		AddChild(Explorer = new ExplorerPanel(resources, preferences) {
			CustomMinimumSize = new Vector2(250, 0)
		});
		Explorer.FileActivated += ExplorerOnFileActivated;
		Explorer.TreeReloaded += Explorer_OnTreeReloaded;
		AddChild(EditArea = new EditArea(resources, project));

		SplitOffset = preferences.ExplorerPanelWidth - (int)Explorer.CustomMinimumSize.X;
		Dragged += SplitContainer_OnDragged;
	}

	private void SplitContainer_OnDragged(long offset) {
		_preferences.ExplorerPanelWidth = SplitOffset + (int)Explorer.CustomMinimumSize.X;
	}

	private void Explorer_OnTreeReloaded() {
		EditArea.ReloadTabNames();
	}

	protected void ExplorerOnFileActivated(FileNavigator file) {
		EditArea.ShowEditorFor(file);
	}
}