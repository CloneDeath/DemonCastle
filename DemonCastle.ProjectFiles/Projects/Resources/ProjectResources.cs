using System;
using System.IO;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using DemonCastle.ProjectFiles.Projects.Migration;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class ProjectResources {
	private DirectoryNavigator Root { get; }

	protected TextFileNavigator GetTextFile(string path) => new(path, this);

	public ProjectResources(string root) {
		Root = new DirectoryNavigator(root);

		AudioStreams = new ResourceCache<AudioStream>(path => {
			var data = File.ReadAllBytes(path);
			var sound = new AudioStreamWav();
			sound.Data = data;
			return sound;
		});

		Characters = new ResourceCache<CharacterInfo>(path
			=> new CharacterInfo(GameFileMigrator.GetFile<CharacterFile>(path)));

		Items = new ResourceCache<ItemInfo>(path
			=> new ItemInfo(GameFileMigrator.GetFile<ItemFile>(path)));

		Fonts = new ResourceCache<Font>(path => {
			var font = new FontFile();
			font.Data = File.ReadAllBytes(path);
			return font;
		});

		Levels = new ResourceCache<LevelInfo>(path
			=> new LevelInfo(GameFileMigrator.GetFile<LevelFile>(path)));

		Monsters = new ResourceCache<MonsterInfo>(path
			=> new MonsterInfo(GameFileMigrator.GetFile<MonsterFile>(path)));

		Projects = new ResourceCache<ProjectInfo>(path
			=> new ProjectInfo(GameFileMigrator.GetFile<ProjectFile>(path)));

		Scenes = new ResourceCache<SceneInfo>(path
			=> new SceneInfo(GameFileMigrator.GetFile<SceneFile>(path)));

		SpriteGrids = new ResourceCache<SpriteGridInfo>(path
			=> new SpriteGridInfo(GameFileMigrator.GetFile<SpriteGridFile>(path)));

		SpriteAtlases = new ResourceCache<SpriteAtlasInfo>(path
			=> new SpriteAtlasInfo(GameFileMigrator.GetFile<SpriteAtlasFile>(path)));

		Texts = new ResourceCache<TextInfo>(path
			=> new TextInfo(GetTextFile(path)));

		TileSets = new ResourceCache<TileSetInfo>(path
			=> new TileSetInfo(GameFileMigrator.GetFile<TileSetFile>(path)));

		Textures = new ResourceCache<Texture2D>(path => {
			var image = new Image();
			image.Load(path);
			return ImageTexture.CreateFromImage(image);
		});

		Weapons = new ResourceCache<WeaponInfo>(path
			=> new WeaponInfo(GameFileMigrator.GetFile<WeaponFile>(path)));
	}

	protected ResourceCache<AudioStream> AudioStreams { get; }
	public AudioStream GetAudioStream(string path) => AudioStreams.Get(path);

	protected ResourceCache<CharacterInfo> Characters { get; }
	public CharacterInfo GetCharacter(FileNavigator path) => Characters.Get(path);

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

	public TileSetInfo GetTileSet(Guid id) => Root.GetFilesAndSubFiles()
												  .Where(f => f.Extension == FileType.TileSet.Extension)
												  .Select(f => f.ToTileSetInfo())
												  .First(t => t.Id == id);

	protected ResourceCache<Texture2D> Textures { get; }
	public Texture2D GetTexture(string path) => Textures.Get(path);

	protected ResourceCache<WeaponInfo> Weapons { get; }
	public WeaponInfo GetWeapon(string path) => Weapons.Get(path);
}