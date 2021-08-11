using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class FrameInfo {
		protected string LocalFile { get; }
		protected FrameData FrameData { get; }

		public FrameInfo(string localFile, FrameData frameData) {
			LocalFile = localFile;
			FrameData = frameData;
		}

		public float Duration => FrameData.Duration;
	}
}