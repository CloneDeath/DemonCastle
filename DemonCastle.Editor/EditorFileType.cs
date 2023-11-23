using System.Collections.Generic;
using DemonCastle.Editor.EditorFileTypes;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor;

public static class EditorFileType {
	public static IEditorFileType Character { get; } = new CharacterEditorFileType();
	public static IEditorFileType Level { get; } = new LevelEditorFileType();
	public static IEditorFileType Monster { get; } = new MonsterEditorFileType();
	public static IEditorFileType SpriteAtlas { get; } = new SpriteAtlasEditorFileType();
	public static IEditorFileType SpriteGrid { get; } = new SpriteGridEditorFileType();
	public static IEditorFileType Weapon { get; } = new WeaponEditorFileType();

	public static IEnumerable<IEditorFileType> CreatableFileTypes { get; } = new[] {
		SpriteAtlas,
		SpriteGrid,
		Character,
		Weapon,
		Level,
		Monster
	};
}

public interface IEditorFileType : IFileType {
	Texture2D Icon { get; }
	object CreateFileInstance();
}