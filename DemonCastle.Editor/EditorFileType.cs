using System.Collections.Generic;
using DemonCastle.Editor.EditorFileTypes;
using DemonCastle.Editor.Editors;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor;

public static class EditorFileType {
	public static IEditorFileType Character { get; } = new CharacterEditorFileType();
	public static IEditorFileType Level { get; } = new LevelEditorFileType();
	public static IEditorFileType Monster { get; } = new MonsterEditorFileType();
	public static IEditorFileType Png { get; } = new PngEditorFileType();
	public static IEditorFileType Project { get; } = new ProjectEditorFileType();
	public static IEditorFileType SpriteAtlas { get; } = new SpriteAtlasEditorFileType();
	public static IEditorFileType SpriteGrid { get; } = new SpriteGridEditorFileType();
	public static IEditorFileType Text { get; } = new TextEditorFileType();
	public static IEditorFileType Weapon { get; } = new WeaponEditorFileType();

	public static IEnumerable<IEditorFileType> CreatableFileTypes { get; } = new[] {
		SpriteAtlas,
		SpriteGrid,
		Character,
		Weapon,
		Level,
		Monster
	};

	public static IEnumerable<IEditorFileType> All { get; } = new[] {
		Character,
		Level,
		Monster,
		Png,
		Project,
		SpriteAtlas,
		SpriteGrid,
		Text,
		Weapon
	};
}

public interface IEditorFileType : IFileType {
	Texture2D Icon { get; }
	object CreateFileInstance();
	BaseEditor GetEditor(FileNavigator file);
}