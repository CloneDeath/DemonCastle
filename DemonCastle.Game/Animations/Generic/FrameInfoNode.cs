using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Game.Animations.Generic;

public partial class FrameInfoNode : TemporalNode {
	private HurtBoxNode? _hurtBox;
	private HitBoxNode? _hitBox;

	public bool AnimationActive { get; set; }

	public FrameInfoNode(IFrameInfo frame, IDamageable owner, DebugState debug) {
		AddChild(new SpriteDefinitionNode(frame.SpriteDefinition, frame.Origin));
		if (frame.HitBox.HasValue) AddChild(_hitBox = new HitBoxNode(frame.HitBox.Value, frame.Origin, owner, debug));
		if (frame.HurtBox.HasValue) AddChild(_hurtBox = new HurtBoxNode(frame.HurtBox.Value, frame.Origin, owner, debug));
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (_hitBox != null) _hitBox.Active = AnimationActive && Active;
		if (_hurtBox != null) _hurtBox.Active = AnimationActive && Active;
	}
}