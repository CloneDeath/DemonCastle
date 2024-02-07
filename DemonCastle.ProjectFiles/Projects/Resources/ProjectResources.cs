using System;
using System.Collections.Generic;
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
	private List<DirectoryNavigator> Directories { get; } = new();
	private List<FileNavigator> Files { get; } = new();

	public ProjectResources(string root) {
		Root = new DirectoryNavigator(root);
		ReloadFiles();
		var migrator = new GameFileMigrator(this);

		AudioStreamCache = new ResourceCache<AudioStream>(file => {
			var data = file.ReadAllBytes();
			var sound = new AudioStreamWav();
			sound.Data = data;
			return sound;
		});
		CharacterCache = new ResourceCache<CharacterInfo>(file => new CharacterInfo(migrator.GetFile<CharacterFile>(file)));
		ItemCache = new ResourceCache<ItemInfo>(file => new ItemInfo(migrator.GetFile<ItemFile>(file)));
		FontCache = new ResourceCache<Font>(file => {
			var font = new FontFile();
			font.Data = file.ReadAllBytes();
			return font;
		});
		LevelCache = new ResourceCache<LevelInfo>(file => new LevelInfo(migrator.GetFile<LevelFile>(file)));
		MonsterCache = new ResourceCache<MonsterInfo>(file => new MonsterInfo(migrator.GetFile<MonsterFile>(file)));
		ProjectCache = new ResourceCache<ProjectInfo>(file => new ProjectInfo(migrator.GetFile<ProjectFile>(file)));
		SceneCache = new ResourceCache<SceneInfo>(file => new SceneInfo(migrator.GetFile<SceneFile>(file)));
		SpriteGridCache = new ResourceCache<SpriteGridInfo>(file => new SpriteGridInfo(migrator.GetFile<SpriteGridFile>(file)));
		SpriteAtlasCache = new ResourceCache<SpriteAtlasInfo>(file => new SpriteAtlasInfo(migrator.GetFile<SpriteAtlasFile>(file)));
		TextCache = new ResourceCache<TextInfo>(file => new TextInfo(file));
		TileSetCache = new ResourceCache<TileSetInfo>(file => new TileSetInfo(migrator.GetFile<TileSetFile>(file)));
		TextureCache = new ResourceCache<Texture2D>(file => {
			var image = new Image();
			image.Load(file.FilePath);
			return ImageTexture.CreateFromImage(image);
		});
		WeaponCache = new ResourceCache<WeaponInfo>(file => new WeaponInfo(migrator.GetFile<WeaponFile>(file)));
	}

	private void ReloadFiles() {
		foreach (var file in Files.Where(file => !file.FileExists()).ToList()) {
			Files.Remove(file);
		}
		foreach (var dir in Directories.Where(dir => !dir.DirectoryExists()).ToList()) {
			Directories.Remove(dir);
		}

		LoadDirectory(Root);
	}

	protected void LoadDirectory(DirectoryNavigator directory) {
		if (directory.DirectoryName.StartsWith(".")) return;

		var current = Directories.FirstOrDefault(d => d.Directory == directory.Directory);
		if (current == null) {
			Directories.Add(directory);
		}

		foreach (var subDirectory in directory.GetDirectories()) {
			LoadDirectory(subDirectory);
		}

		foreach (var file in directory.GetFiles()) {
			LoadFile(file);
		}
	}

	protected void LoadFile(FileNavigator file) {
		var existing = Files.FirstOrDefault(f => f.FilePath == file.FilePath);
		if (existing != null) return;
		Files.Add(file);
	}

	protected ResourceCache<AudioStream> AudioStreamCache { get; }
	public AudioStream GetAudioStream(FileNavigator file) => AudioStreamCache.Get(file);
	public AudioStream GetAudioStream(string path) => AudioStreamCache.Get(GetNavigator(path));

	protected ResourceCache<CharacterInfo> CharacterCache { get; }
	public CharacterInfo GetCharacter(FileNavigator file) => CharacterCache.Get(file);
	public CharacterInfo GetCharacter(string path) => CharacterCache.Get(GetNavigator(path));

	protected ResourceCache<ItemInfo> ItemCache { get; }
	public ItemInfo GetItem(FileNavigator file) => ItemCache.Get(file);
	public ItemInfo GetItem(string path) => ItemCache.Get(GetNavigator(path));
	public IEnumerable<ItemInfo> Items => GetFilesAndSubFiles()
											  .Where(f => f.Extension == FileType.Item.Extension)
											  .Select(f => ItemCache.Get(f));
	public ItemInfo? GetItem(Guid itemId) => Items.FirstOrDefault(i => i.Id == itemId);

	protected ResourceCache<Font> FontCache { get; }
	public Font GetFont(FileNavigator file) => FontCache.Get(file);
	public Font GetFont(string path) => FontCache.Get(GetNavigator(path));

	protected ResourceCache<LevelInfo> LevelCache { get; }
	public LevelInfo GetLevel(FileNavigator file) => LevelCache.Get(file);
	public LevelInfo GetLevel(string path) => LevelCache.Get(GetNavigator(path));

	protected ResourceCache<MonsterInfo> MonsterCache { get; }
	public MonsterInfo GetMonster(FileNavigator file) => MonsterCache.Get(file);
	public MonsterInfo GetMonster(string path) => MonsterCache.Get(GetNavigator(path));
	public IEnumerable<MonsterInfo> Monsters => GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.Monster.Extension)
												.Select(f => MonsterCache.Get(f));
	public MonsterInfo? GetMonster(Guid monsterId) => Monsters.FirstOrDefault(m => m.Id == monsterId);

	protected ResourceCache<ProjectInfo> ProjectCache { get; }
	public ProjectInfo GetProject(FileNavigator file) => ProjectCache.Get(file);
	public ProjectInfo GetProject(string path) => ProjectCache.Get(GetNavigator(path));

	protected ResourceCache<SceneInfo> SceneCache { get; }
	public SceneInfo GetScene(FileNavigator file) => SceneCache.Get(file);
	public SceneInfo GetScene(string path) => SceneCache.Get(GetNavigator(path));

	protected ResourceCache<SpriteGridInfo> SpriteGridCache { get; }
	public SpriteGridInfo GetSpriteGrid(FileNavigator file) => SpriteGridCache.Get(file);
	public SpriteGridInfo GetSpriteGrid(string path) => SpriteGridCache.Get(GetNavigator(path));

	protected ResourceCache<SpriteAtlasInfo> SpriteAtlasCache { get; }
	public SpriteAtlasInfo GetSpriteAtlas(FileNavigator file) => SpriteAtlasCache.Get(file);
	public SpriteAtlasInfo GetSpriteAtlas(string path) => SpriteAtlasCache.Get(GetNavigator(path));

	protected ResourceCache<TextInfo> TextCache { get; }
	public TextInfo GetText(FileNavigator file) => TextCache.Get(file);
	public TextInfo GetText(string path) => TextCache.Get(GetNavigator(path));

	protected ResourceCache<TileSetInfo> TileSetCache { get; }
	public TileSetInfo GetTileSet(FileNavigator file) => TileSetCache.Get(file);
	public TileSetInfo GetTileSet(string path) => TileSetCache.Get(GetNavigator(path));
	public IEnumerable<TileSetInfo> TileSets => GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.TileSet.Extension)
												.Select(f => TileSetCache.Get(f));
	public TileSetInfo? GetTileSet(Guid tileSetId) => TileSets.FirstOrDefault(t => t.Id == tileSetId);

	protected ResourceCache<Texture2D> TextureCache { get; }
	public Texture2D GetTexture(FileNavigator file) => TextureCache.Get(file);
	public Texture2D GetTexture(string path) => TextureCache.Get(GetNavigator(path));

	protected ResourceCache<WeaponInfo> WeaponCache { get; }
	public WeaponInfo GetWeapon(FileNavigator file) => WeaponCache.Get(file);
	public WeaponInfo GetWeapon(string path) => WeaponCache.Get(GetNavigator(path));

	private FileNavigator GetNavigator(string path) {
		var existing = Files.FirstOrDefault(f => f.FilePath == path);
		if (existing != null) return existing;

		var result = new FileNavigator(path);
		Files.Add(result);
		return result;
	}

	private IEnumerable<FileNavigator> GetFilesAndSubFiles() {
		ReloadFiles();
		return Files;
	}
}