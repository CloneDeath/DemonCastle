using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo : INotifyPropertyChanged {
	float Duration { get; }
	ISpriteDefinition SpriteDefinition { get; }
	Vector2I Anchor { get; }
	Vector2I Offset { get; }
}