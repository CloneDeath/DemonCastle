using DemonCastle.Editor.FileTreeView;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorWorkspace {
		
		protected HSplitContainer SplitContainer { get; }
		protected ExplorerPanel Explorer { get; }
		protected EditArea EditArea { get; }
		
		public EditorWorkspace(ProjectInfo project) {
			Name = nameof(EditorWorkspace);
			
			AddChild(SplitContainer = new HSplitContainer {
				Name = nameof(HSplitContainer),
				AnchorRight = 1,
				AnchorBottom = 1
			});

			SplitContainer.AddChild(Explorer = new ExplorerPanel(project.File) {
				CustomMinimumSize = new Vector2(250, 0)
			});
			Explorer.FileActivated += ExplorerOnFileActivated;
			SplitContainer.AddChild(EditArea = new EditArea());
		}
	}
}