using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Game.Animations.Generic;

public partial class FrameInfoNode : TemporalNode {
	public FrameInfoNode(IFrameInfo frame) {
		AddChild(new SpriteDefinitionNode(frame.SpriteDefinition, frame.Origin));
		if (frame.HitBox.HasValue) AddChild(new HitBoxNode(frame.HitBox.Value));
	}
}