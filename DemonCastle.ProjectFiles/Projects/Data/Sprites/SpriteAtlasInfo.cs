using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Extensions;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteAtlasInfo : FileInfo<SpriteAtlasFile>, ISpriteSource, INotifyPropertyChanged {
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
		}
	}

	public Color TransparentColor {
		get => Resource.TransparentColor.ToColor();
		set {
			Resource.TransparentColor = value.ToColorData();
			Save();
			OnPropertyChanged();
		}
	}

	public Texture2D Texture => File.GetTexture(Resource.File);

	public SpriteAtlasDataInfo CreateSprite() {
		var lastSprite = Resource.Sprites.LastOrDefault();
		var spriteAtlasData = new SpriteAtlasData {
			Height = lastSprite?.Height ?? 16,
			Width = lastSprite?.Width ?? 16
		};
		Resource.Sprites.Add(spriteAtlasData);
		var spriteAtlasDataInfo = new SpriteAtlasDataInfo(this, spriteAtlasData);
		_spriteData.Add(spriteAtlasDataInfo);
		OnPropertyChanged(nameof(Sprites));
		OnPropertyChanged(nameof(AtlasSprites));
		return spriteAtlasDataInfo;
	}

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}