using System;
using System.ComponentModel;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IAnimationInfo : INotifyPropertyChanged {
	Guid Id { get; }
	string Name { get; set; }

	IEnumerableInfo<IFrameInfo> Frames { get; }
}