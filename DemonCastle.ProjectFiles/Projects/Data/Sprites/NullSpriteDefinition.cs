using System;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteDefinition : ISpriteDefinition {
	public Guid Id => Guid.Empty;
	public string Name => "null";
	public Texture2D Texture => new GradientTexture2D {
		Gradient = new Gradient {
			Colors = new []{Colors.Red, Colors.Blue},
			Offsets = new []{0f, 1f}
		},
		Width = 16
	};
	public Rect2I Region => new(0, 0, 16, 16);
	public bool FlipHorizontal => false;
	public Color TransparentColor => Colors.Transparent;
	public float TransparentColorThreshold => 0.01f;
}