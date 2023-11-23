using System;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IAnimationInfo {
	Guid Id { get; }
	IObservableCollection<IFrameInfo> Frames { get; }

	void AddFrame();
}