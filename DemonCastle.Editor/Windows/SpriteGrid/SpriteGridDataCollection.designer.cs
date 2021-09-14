using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public partial class SpriteGridDataCollection {
		protected VBoxContainer SpriteCollection { get; }
		protected VBoxContainer Contents { get; }

		public SpriteGridDataCollection(SpriteGridInfo spriteGrid) {
			SpriteGrid = spriteGrid;
			
			Name = nameof(SpriteGridDataCollection);
			AnchorBottom = 1;
			// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
			SizeFlagsVertical = (int) (SizeFlags.Fill | SizeFlags.Expand);
			
			AddChild(Contents = new VBoxContainer {
				//AnchorRight = 1,
				// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
				SizeFlagsHorizontal = (int) (SizeFlags.Fill | SizeFlags.Expand)
			});

			Contents.AddChild(SpriteCollection = new VBoxContainer {
				AnchorRight = 1,
				// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
				SizeFlagsHorizontal = (int) (SizeFlags.Fill | SizeFlags.Expand)
			});
			foreach (var data in spriteGrid.SpriteData) {
				SpriteCollection.AddChild(new SpriteGridDataPanel(data));
			}
			
			Contents.AddChild(new Button {
				Text = "Add Sprite Data"
			});
		}
	}
}