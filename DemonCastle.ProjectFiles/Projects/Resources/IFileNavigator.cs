using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public interface IFileNavigator {
	void Save();
	string Directory { get; }
	bool FileExists(string source);
	ISpriteSource GetSprite(string source);
}