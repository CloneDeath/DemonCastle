using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class HitBoxNode : Area2D {
	public HitBoxNode(Rect2I region) {
		Name = nameof(HitBoxNode);

		AddChild(new CollisionShape2D {
			Position = region.Position,
			Shape = new RectangleShape2D {
				Size = region.Size
			}
		});
		AddChild(new ColorRect {
			Position = region.Position,
			Size = region.Size,
			Color = new Color(Colors.Orange, 0.5f)
		});
	}
}