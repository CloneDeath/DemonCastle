using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class AnimationInfo {
		protected string LocalFile { get; }
		protected AnimationData Animation { get; }

		public AnimationInfo(string localFile, AnimationData animation) {
			LocalFile = localFile;
			Animation = animation;
		}

		public string Name => Animation.Name;
		public IEnumerable<FrameInfo> Frames => Animation.Frames.Select(f => new FrameInfo(LocalFile, f));
	}
}