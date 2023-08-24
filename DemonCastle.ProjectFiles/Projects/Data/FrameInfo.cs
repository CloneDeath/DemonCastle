using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class FrameInfo {
		protected AnimationInfo Animation { get; }
		protected FileNavigator<CharacterFile> File { get; }
		public string Directory => File.Directory;
		protected FrameData FrameData { get; }
		public int Index { get; }

		public FrameInfo(AnimationInfo animation, FileNavigator<CharacterFile> file, FrameData frameData, int index) {
			Animation = animation;
			File = file;
			FrameData = frameData;
			Index = index;
		}

		public float Duration {
			get => FrameData.Duration;
			set { FrameData.Duration = value; Save(); }
		}

		public string SourceFile {
			get => FrameData.Source;
			set { FrameData.Source = value; Save(); }
		}

		public string SpriteName {
			get => FrameData.Sprite;
			set { FrameData.Sprite = value; Save(); }
		}

		protected ISpriteSource Source => string.IsNullOrWhiteSpace(FrameData.Source) ? new NullSpriteSource() : File.GetSprite(FrameData.Source);

		public SpriteInfoNode Sprite => new(Source.GetSpriteDefinition(FrameData.Sprite));
		public TextureRect TextureRect => new SpriteDefinitionTextureRect(Source.GetSpriteDefinition(FrameData.Sprite));

		protected void Save() => File.Save();

		public void Delete() {
			Animation.RemoveFrame(this, FrameData);
		}
	}
}