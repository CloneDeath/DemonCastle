using System;
using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IAnimationInfo {
	Guid Id { get; }
	IEnumerable<IFrameInfo> Frames { get; }
}