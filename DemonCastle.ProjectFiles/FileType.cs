using DemonCastle.ProjectFiles.FileTypes;

namespace DemonCastle.ProjectFiles;

public static class FileType {
	public static IFileTypeData Png => new PngFileTypeData();
	public static IFileTypeData SpriteAtlas => new SpriteAtlasFileTypeData();
	public static IFileTypeData SpriteGrid => new SpriteGridFileTypeData();
	public static IFileTypeData Character => new CharacterFileTypeData();
	public static IFileTypeData Level => new LevelFileTypeData();

	public static IFileTypeData[] SpriteSources => new[] { SpriteAtlas, SpriteGrid };
	public static IFileTypeData[] RawTextureFiles => new[] { Png };
}