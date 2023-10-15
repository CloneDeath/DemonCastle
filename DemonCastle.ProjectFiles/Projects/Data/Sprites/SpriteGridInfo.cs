using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteGridInfo : FileInfo<SpriteGridFile>, ISpriteSource, INotifyPropertyChanged {
	public SpriteGridInfo(FileNavigator<SpriteGridFile> file) : base(file) {
		SpriteData = Resource.Sprites.Select(s => new SpriteGridDataInfo(this, s)).ToList();
	}

	public List<SpriteGridDataInfo> SpriteData { get; }

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

	public ISpriteDefinition GetSpriteDefinition(string spriteName) {
		return SpriteData.FirstOrDefault(s => s.Name == spriteName)
			   ?? (ISpriteDefinition)new NullSpriteDefinition();
	}

	public void AddNewSpriteData() {
		var spriteData = new SpriteGridData();
		Resource.Sprites.Add(spriteData);
		SpriteData.Add(new SpriteGridDataInfo(this, spriteData));
		Save();
	}

	public void Remove(SpriteGridData data, SpriteGridDataInfo spriteGridDataInfo) {
		Resource.Sprites.Remove(data);
		SpriteData.Remove(spriteGridDataInfo);
		Save();
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