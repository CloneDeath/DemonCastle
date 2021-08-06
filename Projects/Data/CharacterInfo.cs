using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class CharacterInfo : ResourceInfo<CharacterFile>, IListableInfo {
		public CharacterInfo(string filePath) : base(filePath) { }
		public string Name => Resource.Name;
	}
}