using System;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

public interface ISpriteDefinition {
	Guid Id { get; }
	string Name { get; }
	Texture2D Texture { get; }
	Rect2I Region { get; }
	bool FlipHorizontal { get; }
	Color TransparentColor { get; }
	float TransparentColorThreshold { get; }
}