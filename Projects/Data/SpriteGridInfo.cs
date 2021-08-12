using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects.Data {
	public class SpriteGridInfo : ISpriteInfo {
		protected FileNavigator<SpriteGridFile> File { get; }
		protected SpriteGridFile Sprite => File.Resource;

		public SpriteGridInfo(FileNavigator<SpriteGridFile> file) {
			File = file;
		}
	}
}