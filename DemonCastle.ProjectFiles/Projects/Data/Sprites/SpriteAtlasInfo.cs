using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteAtlasInfo : FileInfo<SpriteAtlasFile>, ISpriteSource {
	private readonly List<SpriteAtlasDataInfo> _spriteData;

	public SpriteAtlasInfo(FileNavigator<SpriteAtlasFile> file) : base(file) {
		_spriteData = Resource.Sprites.Select(s => new SpriteAtlasDataInfo(this, s)).ToList();
	}

	public IEnumerable<SpriteAtlasDataInfo> AtlasSprites => _spriteData;
	public IEnumerable<ISpriteDefinition> Sprites => _spriteData;

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

	public SpriteAtlasDataInfo CreateSprite() {
		var lastSprite = Resource.Sprites.LastOrDefault();
		var spriteAtlasData = new SpriteAtlasData {
			X = lastSprite?.X + lastSprite?.Width ?? 0,
			Y = lastSprite?.Y ?? 0,
			Height = lastSprite?.Height ?? 16,
			Width = lastSprite?.Width ?? 16
		};
		Resource.Sprites.Add(spriteAtlasData);
		var spriteAtlasDataInfo = new SpriteAtlasDataInfo(this, spriteAtlasData);
		_spriteData.Add(spriteAtlasDataInfo);
		Save();
		OnPropertyChanged(nameof(Sprites));
		OnPropertyChanged(nameof(AtlasSprites));
		return spriteAtlasDataInfo;
	}

	public void DeleteSprite(SpriteAtlasDataInfo sprite) {
		Resource.Sprites.Remove(sprite.Data);
		_spriteData.Remove(sprite);
		Save();
		OnPropertyChanged(nameof(Sprites));
		OnPropertyChanged(nameof(AtlasSprites));
	}
}