using System.IO;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using DemonCastle.ProjectFiles.Projects.Migration;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class ProjectResources {
	protected TextFileNavigator GetTextFile(string path) => new(path, this);

	public ProjectResources() {
		var migrator = new GameFileMigrator(this);

		AudioStreams = new ResourceCache<AudioStream>(path => {
			var data = File.ReadAllBytes(path);
			var sound = new AudioStreamWav();
			sound.Data = data;
			return sound;
		});

		Characters = new ResourceCache<CharacterInfo>(path
			=> new CharacterInfo(migrator.GetFile<CharacterFile>(path)));

		Items = new ResourceCache<ItemInfo>(path
			=> new ItemInfo(migrator.GetFile<ItemFile>(path)));

		Fonts = new ResourceCache<Font>(path => {
			var font = new FontFile();
			font.Data = File.ReadAllBytes(path);
			return font;
		});

		Levels = new ResourceCache<LevelInfo>(path
			=> new LevelInfo(migrator.GetFile<LevelFile>(path)));

		Monsters = new ResourceCache<MonsterInfo>(path
			=> new MonsterInfo(migrator.GetFile<MonsterFile>(path)));

		Projects = new ResourceCache<ProjectInfo>(path
			=> new ProjectInfo(migrator.GetFile<ProjectFile>(path)));

		Scenes = new ResourceCache<SceneInfo>(path
			=> new SceneInfo(migrator.GetFile<SceneFile>(path)));

		SpriteGrids = new ResourceCache<SpriteGridInfo>(path
			=> new SpriteGridInfo(migrator.GetFile<SpriteGridFile>(path)));

		SpriteAtlases = new ResourceCache<SpriteAtlasInfo>(path
			=> new SpriteAtlasInfo(migrator.GetFile<SpriteAtlasFile>(path)));

		Texts = new ResourceCache<TextInfo>(path
			=> new TextInfo(GetTextFile(path)));

		TileSets = new ResourceCache<TileSetInfo>(path
			=> new TileSetInfo(migrator.GetFile<TileSetFile>(path)));

		Textures = new ResourceCache<Texture2D>(path => {
			var image = new Image();
			image.Load(path);
			return ImageTexture.CreateFromImage(image);
		});

		Weapons = new ResourceCache<WeaponInfo>(path
			=> new WeaponInfo(migrator.GetFile<WeaponFile>(path)));
	}

	protected ResourceCache<AudioStream> AudioStreams { get; }
	public AudioStream GetAudioStream(string path) => AudioStreams.Get(path);

	protected ResourceCache<CharacterInfo> Characters { get; }
	public CharacterInfo GetCharacter(string path) => Characters.Get(path);

	protected ResourceCache<ItemInfo> Items { get; }
	public ItemInfo GetItem(string path) => Items.Get(path);

	protected ResourceCache<Font> Fonts { get; }
	public Font GetFont(string path) => Fonts.Get(path);

	protected ResourceCache<LevelInfo> Levels { get; }
	public LevelInfo GetLevel(string path) => Levels.Get(path);

	protected ResourceCache<MonsterInfo> Monsters { get; }
	public MonsterInfo GetMonster(string path) => Monsters.Get(path);

	protected ResourceCache<ProjectInfo> Projects { get; }
	public ProjectInfo GetProject(string path) => Projects.Get(path);

	protected ResourceCache<SceneInfo> Scenes { get; }
	public SceneInfo GetScene(string path) => Scenes.Get(path);

	protected ResourceCache<SpriteGridInfo> SpriteGrids { get; }
	public SpriteGridInfo GetSpriteGrid(string path) => SpriteGrids.Get(path);

	protected ResourceCache<SpriteAtlasInfo> SpriteAtlases { get; }
	public SpriteAtlasInfo GetSpriteAtlas(string path) => SpriteAtlases.Get(path);

	protected ResourceCache<TextInfo> Texts { get; }
	public TextInfo GetText(string path) => Texts.Get(path);

	protected ResourceCache<TileSetInfo> TileSets { get; }
	public TileSetInfo GetTileSet(string path) => TileSets.Get(path);

	protected ResourceCache<Texture2D> Textures { get; }
	public Texture2D GetTexture(string path) => Textures.Get(path);

	protected ResourceCache<WeaponInfo> Weapons { get; }
	public WeaponInfo GetWeapon(string path) => Weapons.Get(path);
}