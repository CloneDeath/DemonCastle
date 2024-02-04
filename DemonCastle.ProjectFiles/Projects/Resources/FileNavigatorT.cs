using System.Threading.Tasks;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class FileNavigator<T> : IFileNavigator {
	private readonly FileNavigator _navigator;
	private readonly ProjectResources _resources;

	public T Resource { get; }
	private Task _saveTask = Task.CompletedTask;

	public FileNavigator(FileNavigator navigator, ProjectResources resources) {
		_navigator = navigator;
		_resources = resources;
		var fileContents = navigator.LoadContent();
		Resource = Serializer.Deserialize<T>(fileContents);
	}

	public bool Saving => _saveTask.IsCompleted;

	public void Save() {
		var contents = Serializer.Serialize(Resource);
		_saveTask = _saveTask.ContinueWith(_ => _navigator.SaveContent(contents));
	}

	public string Directory => _navigator.Directory;
	public string FilePath => _navigator.FilePath;
	public string FileName => _navigator.FileName;
	public bool FileExists(string source) => _navigator.FileExists(source);

	public AudioStream ToAudioStream() => _resources.GetAudioStream(FilePath);
	public CharacterInfo ToCharacterInfo() => _resources.GetCharacter(FilePath);
	public ItemInfo ToItemInfo() => _resources.GetItem(FilePath);
	public Font ToFont() => _resources.GetFont(FilePath);
	public LevelInfo ToLevelInfo() => _resources.GetLevel(FilePath);
	public MonsterInfo ToMonsterInfo() => _resources.GetMonster(FilePath);
	public ProjectInfo ToProjectInfo() => _resources.GetProject(FilePath);
	public SceneInfo ToSceneInfo() => _resources.GetScene(FilePath);
	public SpriteAtlasInfo ToSpriteAtlasInfo() => _resources.GetSpriteAtlas(FilePath);
	public SpriteGridInfo ToSpriteGridInfo() => _resources.GetSpriteGrid(FilePath);
	public TextInfo ToTextInfo() => _resources.GetText(FilePath);
	public TileSetInfo ToTileSetInfo() => _resources.GetTileSet(FilePath);
	public Texture2D ToTexture() => _resources.GetTexture(FilePath);
	public WeaponInfo ToWeaponInfo() => _resources.GetWeapon(FilePath);
}