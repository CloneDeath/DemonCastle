using System.Collections.Generic;
using DemonCastle.ProjectFiles.FileTypes;

namespace DemonCastle.ProjectFiles;

public static class FileType {
	public static IFileType Aseprite => new AsepriteFileType();
	public static IFileType Character => new CharacterFileType();
	public static IFileType Item => new ItemFileType();
	public static IFileType Jpeg => new JpegFileType();
	public static IFileType Jpg => new JpgFileType();
	public static IFileType Level => new LevelFileType();
	public static IFileType Md => new MdFileType();
	public static IFileType Monster => new MonsterFileType();
	public static IFileType Png => new PngFileType();
	public static IFileType Project => new ProjectFileType();
	public static IFileType Scene => new SceneFileType();
	public static IFileType SpriteAtlas => new SpriteAtlasFileType();
	public static IFileType SpriteGrid => new SpriteGridFileType();
	public static IFileType Text => new TextFileType();
	public static IFileType Ttf => new TtfFileType();
	public static IFileType Wav => new WavFileType();
	public static IFileType Weapon => new WeaponFileType();

	public static IEnumerable<IFileType> AudioSources => new[] { Wav };
	public static IEnumerable<IFileType> SpriteSources => new[] { SpriteAtlas, SpriteGrid };
	public static IEnumerable<IFileType> RawTextureFiles => new[] { Jpeg, Jpg, Png };
	public static IEnumerable<IFileType> FontFiles => new[] { Ttf };
}

public interface IFileType {
	string Name { get; }
	string Extension { get; }
	string Filter { get; }
}