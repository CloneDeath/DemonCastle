using DemonCastle.Game.DebugNodes;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class HurtBoxNode : Area2D {
	public HurtBoxNode(Rect2I region, DebugState debug) {
		Name = nameof(HurtBoxNode);

		AddChild(new CollisionShape2D {
			//Position = region.Position,
			Shape = new RectangleShape2D {
				Size = region.Size
			},
			DebugColor = new Color(Colors.Orange, 0.5f),
			Visible = debug.ShowHurtBoxes
		});
	}
}