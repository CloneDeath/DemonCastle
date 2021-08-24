using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace {
		protected HSplitContainer SplitContainer { get; }
		protected FileTree FileTree { get; }
		
		public EditorSpace(ProjectInfo project) {
			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
			});

			SplitContainer.AddChild(FileTree = new FileTree(project.File) {
				RectMinSize = new Vector2(250, 0)
			});
			FileTree.OnItemActivated += FileTreeOnOnItemActivated;
			SplitContainer.AddChild(new Control());
		}
	}
}