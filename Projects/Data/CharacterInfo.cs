using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class CharacterInfo : ResourceInfo<CharacterFile>, IListableInfo {
		public CharacterInfo(string filePath) : base(filePath) { }
		
		public string Name => Resource.Name;
		public float WalkSpeed => Resource.WalkSpeed;
		public IEnumerable<AnimationInfo> Animations => Resource.Animations.Select(data => new AnimationInfo(FilePath, data));
	}
}