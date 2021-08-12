using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects.Data {
	public class AnimationInfo {
		protected DirectoryNavigator Directory { get; }
		protected AnimationData Animation { get; }

		public AnimationInfo(DirectoryNavigator directory, AnimationData animation) {
			Directory = directory;
			Animation = animation;
		}

		public string Name => Animation.Name;
		public IEnumerable<FrameInfo> Frames => Animation.Frames.Select(f => new FrameInfo(Directory, f));
	}
}