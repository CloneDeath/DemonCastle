using DemonCastle.Editor.FileTreeView;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorWorkspace {
		
		protected HSplitContainer SplitContainer { get; }
		protected FileTree FileTree { get; }
		protected WindowContainer WindowContainer { get; }
		
		public EditorWorkspace(ProjectInfo project) {
			Name = nameof(EditorWorkspace);
			
			AddChild(SplitContainer = new HSplitContainer {
				Name = nameof(HSplitContainer),
				AnchorRight = 1,
				AnchorBottom = 1,
			});

			SplitContainer.AddChild(FileTree = new FileTree(project.File) {
				RectMinSize = new Vector2(250, 0)
			});
			FileTree.OnItemActivated += FileTreeOnOnItemActivated;
			SplitContainer.AddChild(WindowContainer = new WindowContainer());
		}
	}
}