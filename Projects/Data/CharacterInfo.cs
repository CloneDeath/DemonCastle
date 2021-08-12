using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects.Data {
	public class CharacterInfo : IListableInfo {
		protected FileNavigator<CharacterFile> File { get; }
		protected CharacterFile Character => File.Resource;
		public CharacterInfo(FileNavigator<CharacterFile> file) {
			File = file;
		}
		
		public string Name => Character.Name;
		public float WalkSpeed => Character.WalkSpeed;
		public IEnumerable<AnimationInfo> Animations => Character.Animations.Select(data => new AnimationInfo(File, data));
		public string IdleAnimation => Character.IdleAnimation;
		public string WalkAnimation => Character.WalkAnimation;
	}
}