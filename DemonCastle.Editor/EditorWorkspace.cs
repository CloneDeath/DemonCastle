using DemonCastle.Editor.FileInfo;
using DemonCastle.Editor.FileTreeView;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorWorkspace : Control {
	private readonly ProjectPreferencesInfo _preferences;
	protected HSplitContainer SplitContainer { get; }
	protected ExplorerPanel Explorer { get; }
	protected EditArea EditArea { get; }

	public EditorWorkspace(ProjectResources resources, ProjectInfo project, ProjectPreferencesInfo preferences) {
		_preferences = preferences;
		Name = nameof(EditorWorkspace);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);

		SplitContainer.AddChild(Explorer = new ExplorerPanel(resources) {
			CustomMinimumSize = new Vector2(250, 0)
		});
		Explorer.FileActivated += ExplorerOnFileActivated;
		Explorer.TreeReloaded += Explorer_OnTreeReloaded;
		SplitContainer.AddChild(EditArea = new EditArea(resources, project));

		SplitContainer.SplitOffset = preferences.ExplorerPanelWidth - (int)Explorer.CustomMinimumSize.X;
		SplitContainer.Dragged += SplitContainer_OnDragged;
	}

	private void SplitContainer_OnDragged(long offset) {
		_preferences.ExplorerPanelWidth = SplitContainer.SplitOffset + (int)Explorer.CustomMinimumSize.X;
	}

	private void Explorer_OnTreeReloaded() {
		EditArea.ReloadTabNames();
	}

	protected void ExplorerOnFileActivated(FileNavigator file) {
		EditArea.ShowEditorFor(file);
	}
}