using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public partial class SpriteGridDataCollection {
		protected VBoxContainer SpriteCollection { get; }
		protected VBoxContainer Contents { get; }
		protected Button AddSpriteDataButton { get; }

		public SpriteGridDataCollection(SpriteGridInfo spriteGrid) {
			SpriteGrid = spriteGrid;
			
			Name = nameof(SpriteGridDataCollection);
			AnchorBottom = 1;
			SizeFlagsVertical = SizeFlags.ExpandFill;
			
			AddChild(Contents = new VBoxContainer {
				SizeFlagsHorizontal = SizeFlags.ExpandFill
			});

			Contents.AddChild(SpriteCollection = new VBoxContainer {
				AnchorRight = 1,
				SizeFlagsHorizontal = SizeFlags.ExpandFill
			});
			ReloadSpriteData();
			
			Contents.AddChild(AddSpriteDataButton = new Button {
				Text = "Add Sprite2D Data"
			});
			AddSpriteDataButton.Connect("pressed", new Callable(this, nameof(OnAddSpriteDataButtonPressed)));
		}
	}
}