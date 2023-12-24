using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public interface IFileNavigator {
	void Save();
	string Directory { get; }
	bool FileExists(string source);
	ISpriteSource GetSprite(string source);
	AudioStream GetAudioStream(string source);
}