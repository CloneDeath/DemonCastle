using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public class SpriteGridDataCollection : ScrollContainer {
		protected VBoxContainer SpriteCollection { get; }
		public SpriteGridDataCollection(IEnumerable<SpriteGridDataInfo> spriteData) {
			Name = nameof(SpriteGridDataCollection);
			AnchorBottom = 1;
			// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
			SizeFlagsVertical = (int) (SizeFlags.Fill | SizeFlags.Expand);

			AddChild(SpriteCollection = new VBoxContainer {
				AnchorRight = 1,
				// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
				SizeFlagsHorizontal = (int) (SizeFlags.Fill | SizeFlags.Expand)
			});
			foreach (var data in spriteData) {
				SpriteCollection.AddChild(new SpriteGridDataPanel(data));
			}
		}
	}
}