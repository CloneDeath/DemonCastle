using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class FrameInfo {
		protected DirectoryNavigator Directory { get; }
		protected FrameData FrameData { get; }
		public int Index { get; }

		public FrameInfo(DirectoryNavigator directory, FrameData frameData, int index) {
			Directory = directory;
			FrameData = frameData;
			Index = index;
		}

		public float Duration => FrameData.Duration;
		protected ISpriteSource Source => string.IsNullOrWhiteSpace(FrameData.Source) ? new NullSpriteSource() : Directory.GetSprite(FrameData.Source);

		public SpriteInfoNode Sprite => new SpriteInfoNode(Source.GetSpriteDefinition(FrameData.Sprite));
		public TextureRect TextureRect => new SpriteDefinitionTextureRect(Source.GetSpriteDefinition(FrameData.Sprite));
	}
}