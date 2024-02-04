using System.Collections.Generic;
using DemonCastle.Editor.EditorFileTypes;
using DemonCastle.Editor.Editors;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor;

public static class EditorFileType {
	public static IEditorFileType Aseprite { get; } = new AsepriteEditorFileType();
	public static IEditorFileType Character { get; } = new CharacterEditorFileType();
	public static IEditorFileType Item { get; } = new ItemEditorFileType();
	public static IEditorFileType Jpeg { get; } = new JpegEditorFileType();
	public static IEditorFileType Jpg { get; } = new JpgEditorFileType();
	public static IEditorFileType Level { get; } = new LevelEditorFileType();
	public static IEditorFileType Md { get; } = new MdEditorFileType();
	public static IEditorFileType Monster { get; } = new MonsterEditorFileType();
	public static IEditorFileType Png { get; } = new PngEditorFileType();
	public static IEditorFileType Project { get; } = new ProjectEditorFileType();
	public static IEditorFileType Scene { get; } = new SceneEditorFileType();
	public static IEditorFileType SpriteAtlas { get; } = new SpriteAtlasEditorFileType();
	public static IEditorFileType SpriteGrid { get; } = new SpriteGridEditorFileType();
	public static IEditorFileType Text { get; } = new TextEditorFileType();
	public static IEditorFileType TileSet { get; } = new TileSetEditorFileType();
	public static IEditorFileType Ttf { get; } = new TtfEditorFileType();
	public static IEditorFileType Wav { get; } = new WavEditorFileType();
	public static IEditorFileType Weapon { get; } = new WeaponEditorFileType();

	public static IEnumerable<IEditorFileType> CreatableFileTypes { get; } = new[] {
		SpriteAtlas,
		SpriteGrid,
		Character,
		Weapon,
		Monster,
		Item,
		TileSet,
		Level,
		Scene
	};

	public static IEnumerable<IEditorFileType> All { get; } = new[] {
		Aseprite,
		Character,
		Item,
		Jpeg,
		Jpg,
		Level,
		Md,
		Monster,
		Png,
		Project,
		Scene,
		SpriteAtlas,
		SpriteGrid,
		Text,
		TileSet,
		Ttf,
		Wav,
		Weapon
	};
}

public interface IEditorFileType : IFileType {
	Texture2D Icon { get; }
	object CreateFileInstance(string name);
	BaseEditor GetEditor(ProjectInfo project, FileNavigator file);
}