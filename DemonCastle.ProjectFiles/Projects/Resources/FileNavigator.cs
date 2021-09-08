using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using Path = System.IO.Path;
using File = System.IO.File;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public class FileNavigator : DirectoryNavigator {
		public string FilePath { get; }
		public string FileName => Path.GetFileName(FilePath);
		public string Extension => Path.GetExtension(FilePath);
		public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);

		public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
		public FileNavigator(string filePath, ProjectResources resources) 
			: base(Path.GetDirectoryName(filePath), resources) {
			FilePath = filePath;
		}

		public void DeleteFile() => File.Delete(FilePath);

		public TextInfo ToTextInfo() => ProjectResources.GetText(FilePath);
		public Texture ToTexture() => ProjectResources.GetTexture(FilePath);
		public SpriteAtlasInfo ToSpriteAtlasInfo() => ProjectResources.GetSpriteAtlas(FilePath);
		public SpriteGridInfo ToSpriteGridInfo() => ProjectResources.GetSpriteGrid(FilePath);
		public CharacterInfo ToCharacterInfo() => ProjectResources.GetCharacter(FilePath);
		public LevelInfo ToLevelInfo() => ProjectResources.GetLevel(FilePath);
		public ProjectInfo ToProjectInfo() => ProjectResources.GetProject(FilePath);

		public void Rename(string newName) {
			var newPath = Path.Combine(Directory, newName);
			File.Move(FilePath, newPath);
		}
	}
}