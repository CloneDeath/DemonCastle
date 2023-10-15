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

	public IEnumerable<SpriteAtlasDataInfo> SpriteData => _spriteData;

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

	public ISpriteDefinition GetSpriteDefinition(string spriteName) {
		return SpriteData.FirstOrDefault(s => s.Name == spriteName)
			   ?? (ISpriteDefinition)new NullSpriteDefinition();
	}

	public SpriteAtlasDataInfo CreateSprite() {
		var spriteAtlasData = new SpriteAtlasData();
		Resource.Sprites.Add(spriteAtlasData);
		_spriteData.Add(new SpriteAtlasDataInfo(this, spriteAtlasData));
		return new SpriteAtlasDataInfo(this, spriteAtlasData);
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