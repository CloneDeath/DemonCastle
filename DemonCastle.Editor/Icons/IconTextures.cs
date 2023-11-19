using Godot;

namespace DemonCastle.Editor.Icons;

public static class IconTextures {
	public static Texture2D AddIcon { get; } = LoadTexture(nameof(AddIcon));
	public static Texture2D CharacterIcon { get; } = LoadTexture(nameof(CharacterIcon));
	public static Texture2D FolderIcon { get; } = LoadTexture(nameof(FolderIcon));
	public static Texture2D GridIcon { get; } = LoadTexture(nameof(GridIcon));
	public static Texture2D LevelIcon { get; } = LoadTexture(nameof(LevelIcon));
	public static Texture2D MagnifyMinusIcon { get; } = LoadTexture(nameof(MagnifyMinusIcon));
	public static Texture2D MagnifyPlusIcon { get; } = LoadTexture(nameof(MagnifyPlusIcon));
	public static Texture2D OneToOneIcon { get; } = LoadTexture(nameof(OneToOneIcon));
	public static Texture2D ProjectIcon { get; } = LoadTexture(nameof(ProjectIcon));
	public static Texture2D RefreshIcon { get; } = LoadTexture(nameof(RefreshIcon));
	public static Texture2D SpriteAtlasIcon { get; } = LoadTexture(nameof(SpriteAtlasIcon));
	public static Texture2D SpriteGridIcon { get; } = LoadTexture(nameof(SpriteGridIcon));
	public static Texture2D TextFileIcon { get; } = LoadTexture(nameof(TextFileIcon));
	public static Texture2D TextureIcon { get; } = LoadTexture(nameof(TextureIcon));
	public static Texture2D UnknownIcon { get; } = LoadTexture(nameof(UnknownIcon));
	public static Texture2D WeaponIcon { get; } = LoadTexture(nameof(WeaponIcon));

	private static Texture2D LoadTexture(string textureName) {
		return ResourceLoader.Load<Texture2D>($"res://DemonCastle.Editor/Icons/{textureName}.png");
	}
}