using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo {
	float Duration { get; }
	ISpriteDefinition SpriteDefinition { get; }
}