using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class HurtBoxNode : Area2D {
	private readonly IDamageable _source;

	public HurtBoxNode(Rect2I region, Vector2 origin, IDamageable source, DebugState debug) {
		_source = source;
		Name = nameof(HurtBoxNode);

		AddChild(new CollisionShape2D {
			Position = -origin + region.Position + region.Size / 2,
			Shape = new RectangleShape2D {
				Size = region.Size
			},
			DebugColor = new Color(Colors.Orange, 0.5f),
			Visible = debug.ShowHurtBoxes
		});
		CollisionLayer = (uint) CollisionLayers.HurtBox;
		CollisionMask = (uint) CollisionLayers.HitBox;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		foreach (var hitBox in GetOverlappingAreas().Cast<HitBoxNode>()) {
			if (hitBox.OwnerId == _source.Id) continue;
			hitBox.TakeDamage(1);
		}
	}
}