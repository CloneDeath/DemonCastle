using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public interface IFileNavigator {
	void Save();
	ISpriteSource GetSprite(string source);
}