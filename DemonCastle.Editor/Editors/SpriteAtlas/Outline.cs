using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class Outline : Control {
	public Outline() {
		// Top
		AddChild(new ColorRect {
			Color = Colors.White,
			AnchorLeft = 0,
			AnchorRight = 1,
			AnchorTop = 0,
			AnchorBottom = 0,
			OffsetTop = -1
		});
		
		// Bottom
		AddChild(new ColorRect {
			Color = Colors.White,
			AnchorLeft = 0,
			AnchorRight = 1,
			AnchorTop = 1,
			AnchorBottom = 1,
			OffsetBottom = 1
		});
		
		// Left
		AddChild(new ColorRect {
			Color = Colors.White,
			AnchorLeft = 0,
			AnchorRight = 0,
			AnchorTop = 0,
			AnchorBottom = 1,
			OffsetLeft = -1
		});
		
		// Right
		AddChild(new ColorRect {
			Color = Colors.White,
			AnchorLeft = 1,
			AnchorRight = 1,
			AnchorTop = 0,
			AnchorBottom = 1,
			OffsetRight = 1
		});
	}
}