using Godot;

namespace DemonCastle.Editor.Icons;

public static class IconTextures {
	public static Texture2D AddFrameIcon { get; } = LoadTexture(nameof(AddFrameIcon));
	public static Texture2D AddIcon { get; } = LoadTexture(nameof(AddIcon));
	public static Texture2D AsepriteIcon { get; } = LoadTexture(nameof(AsepriteIcon));
	public static Texture2D CharacterIcon { get; } = LoadTexture(nameof(CharacterIcon));
	public static Texture2D CollapseIcon { get; } = LoadTexture(nameof(CollapseIcon));
	public static Texture2D ColorRectElementIcon { get; } = LoadTexture(nameof(ColorRectElementIcon));
	public static Texture2D DeleteIcon { get; } = LoadTexture(nameof(DeleteIcon));
	public static Texture2D DownIcon { get; } = LoadTexture(nameof(DownIcon));
	public static Texture2D ExpandIcon { get; } = LoadTexture(nameof(ExpandIcon));
	public static Texture2D FolderIcon { get; } = LoadTexture(nameof(FolderIcon));
	public static Texture2D FontIcon { get; } = LoadTexture(nameof(FontIcon));
	public static Texture2D GridIcon { get; } = LoadTexture(nameof(GridIcon));
	public static Texture2D HealthBarElementIcon { get; } = LoadTexture(nameof(HealthBarElementIcon));
	public static Texture2D ItemIcon { get; } = LoadTexture(nameof(ItemIcon));
	public static Texture2D LabelElementIcon { get; } = LoadTexture(nameof(LabelElementIcon));
	public static Texture2D LevelIcon { get; } = LoadTexture(nameof(LevelIcon));
	public static Texture2D LevelViewElementIcon { get; } = LoadTexture(nameof(LevelViewElementIcon));
	public static Texture2D MagnifyMinusIcon { get; } = LoadTexture(nameof(MagnifyMinusIcon));
	public static Texture2D MagnifyPlusIcon { get; } = LoadTexture(nameof(MagnifyPlusIcon));
	public static Texture2D MonsterIcon { get; } = LoadTexture(nameof(MonsterIcon));
	public static Texture2D OneToOneIcon { get; } = LoadTexture(nameof(OneToOneIcon));
	public static Texture2D OptionListElementIcon { get; } = LoadTexture(nameof(OptionListElementIcon));
	public static Texture2D ProjectIcon { get; } = LoadTexture(nameof(ProjectIcon));
	public static Texture2D RefreshIcon { get; } = LoadTexture(nameof(RefreshIcon));
	public static Texture2D SceneIcon { get; } = LoadTexture(nameof(SceneIcon));
	public static Texture2D SoundIcon { get; } = LoadTexture(nameof(SoundIcon));
	public static Texture2D SpriteAtlasIcon { get; } = LoadTexture(nameof(SpriteAtlasIcon));
	public static Texture2D SpriteElementIcon { get; } = LoadTexture(nameof(SpriteElementIcon));
	public static Texture2D SpriteGridIcon { get; } = LoadTexture(nameof(SpriteGridIcon));
	public static Texture2D TextFileIcon { get; } = LoadTexture(nameof(TextFileIcon));
	public static Texture2D TextureIcon { get; } = LoadTexture(nameof(TextureIcon));
	public static Texture2D UnknownIcon { get; } = LoadTexture(nameof(UnknownIcon));
	public static Texture2D UpIcon { get; } = LoadTexture(nameof(UpIcon));
	public static Texture2D WeaponIcon { get; } = LoadTexture(nameof(WeaponIcon));

	private static Texture2D LoadTexture(string textureName) {
		return ResourceLoader.Load<Texture2D>($"res://DemonCastle.Editor/Icons/{textureName}.png");
	}
}