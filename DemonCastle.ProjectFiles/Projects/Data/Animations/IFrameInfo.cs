using System;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo : INotifyPropertyChanged {
	float Duration { get; }
	Vector2I Anchor { get; }
	Vector2I Offset { get; }

	string SourceFile { get; }
	Guid SpriteId { get; }
	ISpriteDefinition SpriteDefinition { get; }
}