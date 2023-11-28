using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public interface IFileNavigator {
	void Save();
	string Directory { get; }
	ISpriteSource GetSprite(string source);
}