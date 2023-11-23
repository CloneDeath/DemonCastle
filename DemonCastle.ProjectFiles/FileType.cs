using System.Collections.Generic;
using DemonCastle.ProjectFiles.FileTypes;

namespace DemonCastle.ProjectFiles;

public static class FileType {
	public static IFileType Png => new PngFileType();
	public static IFileType SpriteAtlas => new SpriteAtlasFileType();
	public static IFileType SpriteGrid => new SpriteGridFileType();
	public static IFileType Text => new TextFileType();
	public static IFileType Character => new CharacterFileType();
	public static IFileType Level => new LevelFileType();
	public static IFileType Weapon => new WeaponFileType();

	public static IEnumerable<IFileType> SpriteSources => new[] { SpriteAtlas, SpriteGrid };
	public static IEnumerable<IFileType> RawTextureFiles => new[] { Png };
}

public interface IFileType {
	string Name { get; }
	string Extension { get; }
	string Filter { get; }
}