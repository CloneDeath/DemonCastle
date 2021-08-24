using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace {
		protected HSplitContainer SplitContainer { get; }
		protected Tree FileTree { get; }
		
		public EditorSpace() {
			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
			});

			SplitContainer.AddChild(FileTree = new Tree {
				RectMinSize = new Vector2(200, 0)
			});
			SplitContainer.AddChild(new Control());
			
			var root = FileTree.CreateItem();
			root.SetText(0, "root");
			var c1 = FileTree.CreateItem(root);
			c1.SetText(0, "c1");
			var c2  = FileTree.CreateItem(root);
			c2.SetText(0, "c2");
		}
	}
}