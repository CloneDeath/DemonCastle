using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteGridInfo : FileInfo<SpriteGridFile>, ISpriteSource, INotifyPropertyChanged {
	private List<SpriteGridDataInfo> SpriteData { get; }

	public SpriteGridInfo(FileNavigator<SpriteGridFile> file) : base(file) {
		SpriteData = Resource.Sprites.Select(s => new SpriteGridDataInfo(this, s)).ToList();
	}

	public IEnumerable<SpriteGridDataInfo> GridSprites => SpriteData;
	public IEnumerable<ISpriteDefinition> Sprites => SpriteData;

	public Texture2D Texture => File.GetTexture(Resource.File);

	public Vector2I Offset => new(Resource.XOffset, Resource.YOffset);
	public Vector2I Span => new(Resource.Width + Resource.XSeparation, Resource.Height + Resource.YSeparation);
	public Vector2I Size => new(Resource.Width, Resource.Height);

	public string SpriteFile {
		get => Resource.File;
		set {
			Resource.File = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Width {
		get => Resource.Width;
		set {
			Resource.Width = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Height {
		get => Resource.Height;
		set {
			Resource.Height = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int XOffset {
		get => Resource.XOffset;
		set {
			Resource.XOffset = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int YOffset {
		get => Resource.YOffset;
		set {
			Resource.YOffset = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int XSeparation {
		get => Resource.XSeparation;
		set {
			Resource.XSeparation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int YSeparation {
		get => Resource.YSeparation;
		set {
			Resource.YSeparation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SpriteGridDataInfo CreateSprite() {
		var spriteData = new SpriteGridData();
		Resource.Sprites.Add(spriteData);
		var spriteGridDataInfo = new SpriteGridDataInfo(this, spriteData);
		SpriteData.Add(spriteGridDataInfo);
		Save();
		OnPropertyChanged(nameof(Sprites));
		OnPropertyChanged(nameof(GridSprites));
		return spriteGridDataInfo;
	}

	public void Remove(SpriteGridData data, SpriteGridDataInfo spriteGridDataInfo) {
		Resource.Sprites.Remove(data);
		SpriteData.Remove(spriteGridDataInfo);
		Save();
		OnPropertyChanged(nameof(Sprites));
		OnPropertyChanged(nameof(GridSprites));
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