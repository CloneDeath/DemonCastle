using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo : INotifyPropertyChanged {
	float Duration { get; }
	ISpriteDefinition SpriteDefinition { get; }
}