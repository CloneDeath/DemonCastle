using System.Collections.Generic;
using DemonCastle.ProjectFiles.FileTypes;

namespace DemonCastle.ProjectFiles;

public static class FileType {
	public static IFileTypeData Png => new PngFileTypeData();
	public static IFileTypeData SpriteAtlas => new SpriteAtlasFileTypeData();
	public static IFileTypeData SpriteGrid => new SpriteGridFileTypeData();
	public static IFileTypeData Character => new CharacterFileTypeData();
	public static IFileTypeData Level => new LevelFileTypeData();
	public static IFileTypeData Weapon => new WeaponFileType();

	public static IEnumerable<IFileTypeData> SpriteSources => new[] { SpriteAtlas, SpriteGrid };
	public static IEnumerable<IFileTypeData> RawTextureFiles => new[] { Png };
}