using Godot;

namespace DemonCastle.Editor.Icons; 

public static class IconTextures {
	public static Texture2D AtlasIcon { get; } = LoadTexture(nameof(AtlasIcon));
	public static Texture2D CharacterIcon { get; } = LoadTexture(nameof(CharacterIcon));
	public static Texture2D FolderIcon { get; } = LoadTexture(nameof(FolderIcon));
	public static Texture2D LevelIcon { get; } = LoadTexture(nameof(LevelIcon));
	public static Texture2D ProjectIcon { get; } = LoadTexture(nameof(ProjectIcon));
	public static Texture2D RefreshIcon { get; } = LoadTexture(nameof(RefreshIcon));
	public static Texture2D SpriteGridIcon { get; } = LoadTexture(nameof(SpriteGridIcon));
	public static Texture2D TextFileIcon { get; } = LoadTexture(nameof(TextFileIcon));
	public static Texture2D TextureIcon { get; } = LoadTexture(nameof(TextureIcon));
	public static Texture2D UnknownIcon { get; } = LoadTexture(nameof(UnknownIcon));
		
	private static Texture2D LoadTexture(string textureName) {
		return ResourceLoader.Load<Texture2D>($"res://DemonCastle.Editor/Icons/{textureName}.png");
	}
}