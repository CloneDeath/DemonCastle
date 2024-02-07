using System;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public interface IFileNavigator {
	void Save();
	string Directory { get; }
	string FilePath { get; }
	string FileName { get; }
	bool FileExists(string source);
	AudioStream GetAudioStream(string source);
	CharacterInfo GetCharacter(string character);
	Font GetFont(string source);
	LevelInfo GetLevel(string level);
	SceneInfo GetScene(string source);
	ISpriteSource GetSprite(string source);
	TileSetInfo? GetTileSet(Guid tileSetId);
}