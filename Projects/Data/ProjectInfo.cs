using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class ProjectInfo : ResourceInfo<ProjectFile>, IListableInfo {
		public ProjectInfo(string filePath) : base(filePath) { }

		public string Name => Resource.Name;
		protected IEnumerable<string> CharacterFiles => Resource.Characters.Select(Files.GetFile);
		public IEnumerable<CharacterInfo> Characters => CharacterFiles.Select(cf => new CharacterInfo(cf));
	}
}