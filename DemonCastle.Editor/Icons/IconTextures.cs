using Godot;

namespace DemonCastle.Editor.Icons {
	public static class IconTextures {
		public static Texture AtlasIcon { get; } = LoadTexture(nameof(AtlasIcon));
		public static Texture CharacterIcon { get; } = LoadTexture(nameof(CharacterIcon));
		public static Texture FolderIcon { get; } = LoadTexture(nameof(FolderIcon));
		public static Texture LevelIcon { get; } = LoadTexture(nameof(LevelIcon));
		public static Texture ProjectIcon { get; } = LoadTexture(nameof(ProjectIcon));
		public static Texture SpriteGridIcon { get; } = LoadTexture(nameof(SpriteGridIcon));
		public static Texture TextFileIcon { get; } = LoadTexture(nameof(TextFileIcon));
		public static Texture TextureIcon { get; } = LoadTexture(nameof(TextureIcon));
		public static Texture UnknownIcon { get; } = LoadTexture(nameof(UnknownIcon));
		
		private static Texture LoadTexture(string textureName) {
			return ResourceLoader.Load<Texture>($"res://DemonCastle.Editor/Icons/{textureName}.png");
		}
	}
}