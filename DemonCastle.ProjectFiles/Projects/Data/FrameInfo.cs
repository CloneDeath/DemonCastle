using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class FrameInfo {
		protected DirectoryNavigator Directory { get; }
		protected FrameData FrameData { get; }

		public FrameInfo(DirectoryNavigator directory, FrameData frameData) {
			Directory = directory;
			FrameData = frameData;
		}

		public float Duration => FrameData.Duration;
		protected ISpriteInfo Source => Directory.GetSprite(FrameData.Source);

		public SpriteInfoNode Sprite => Source.GetSprite(FrameData.Sprite);
	}
}