using System;
using System.ComponentModel;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

public interface ISpriteDefinition : INotifyPropertyChanged {
	Guid Id { get; }
	string Name { get; }
	Texture2D Texture { get; }
	Rect2I Region { get; }
	bool FlipHorizontal { get; }
	Color TransparentColor { get; }
	float TransparentColorThreshold { get; }
}

public static class SpriteDefinitionExtensions {
	public static Texture2D ToTexture(this ISpriteDefinition self) {
		return new AtlasTexture {
			Atlas = self.Texture,
			Region = self.Region,
			FilterClip = true
		};
	}
}