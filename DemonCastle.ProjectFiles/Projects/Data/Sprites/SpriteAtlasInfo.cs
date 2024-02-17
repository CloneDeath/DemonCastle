using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteAtlasInfo : FileInfo<SpriteAtlasFile>, ISpriteSource {
	public SpriteAtlasInfo(FileNavigator<SpriteAtlasFile> file) : base(file) {
		AtlasSprites = new SpriteDefinitionCollection<SpriteAtlasDataInfo, SpriteAtlasData>(file, new SpriteAtlasInfoFactory(file, this), Resource.Sprites);
	}

	public SpriteDefinitionCollection<SpriteAtlasDataInfo, SpriteAtlasData> AtlasSprites { get; }
	public IEnumerableInfo<ISpriteDefinition> Sprites => AtlasSprites;

	public string SpriteFile {
		get => Resource.File;
		set {
			Resource.File = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Sprites));
			OnPropertyChanged(nameof(AtlasSprites));
		}
	}

	public Color TransparentColor {
		get => Resource.TransparentColor.ToColor();
		set {
			Resource.TransparentColor = value.ToColorData();
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Sprites));
			OnPropertyChanged(nameof(AtlasSprites));
		}
	}

	public Texture2D Texture => File.GetTexture(Resource.File);
}