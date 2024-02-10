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

	public bool Active { get; set; }

	public override void _Process(double delta) {
		base._Process(delta);
		if (!Active) return;
		var otherHitBoxes = GetOverlappingAreas().Cast<HitBoxNode>()
												 .Where(n => n.OwnerId != _source.Id)
												 .Where(n => n.Active);
		foreach (var hitBox in otherHitBoxes) {
			hitBox.TakeDamage(1);
		}
	}
}