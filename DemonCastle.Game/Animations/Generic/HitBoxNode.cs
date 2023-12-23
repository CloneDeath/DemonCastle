using System;
using DemonCastle.Game.DebugNodes;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class HitBoxNode : Area2D {
	private readonly IDamageable _owner;

	public HitBoxNode(Rect2I region, Vector2 origin, IDamageable owner, DebugState debug) {
		_owner = owner;
		Name = nameof(HitBoxNode);

		AddChild(new CollisionShape2D {
			Position = -origin + region.Position + region.Size / 2,
			Shape = new RectangleShape2D {
				Size = region.Size
			},
			DebugColor = new Color(Colors.Cyan, 0.5f),
			Visible = debug.ShowHitBoxes
		});
	}

	public Guid OwnerId => _owner.Id;

	public void TakeDamage(int amount) {
		_owner.TakeDamage(amount);
	}
}