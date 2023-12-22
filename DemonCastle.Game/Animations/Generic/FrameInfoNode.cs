using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Game.Animations.Generic;

public partial class FrameInfoNode : TemporalNode {
	public FrameInfoNode(IFrameInfo frame, DebugState debug) {
		AddChild(new SpriteDefinitionNode(frame.SpriteDefinition, frame.Origin));
		if (frame.HitBox.HasValue) AddChild(new HitBoxNode(frame.HitBox.Value, debug));
		if (frame.HurtBox.HasValue) AddChild(new HitBoxNode(frame.HurtBox.Value, debug));
	}
}