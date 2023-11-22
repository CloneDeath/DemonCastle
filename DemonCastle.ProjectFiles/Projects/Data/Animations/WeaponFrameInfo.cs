using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponFrameInfo : INotifyPropertyChanged {
	public WeaponFrameInfo(WeaponAnimationInfo animation, FileNavigator<WeaponFile> file, WeaponFrameData frameData, int index) {
		Animation = animation;
		File = file;
		FrameData = frameData;
		Index = index;
	}

	protected WeaponAnimationInfo Animation { get; }
	protected FileNavigator<WeaponFile> File { get; }
	public string Directory => File.Directory;
	public WeaponFrameData FrameData { get; }
	public int Index { get; }

	public float Duration {
		get => FrameData.Duration;
		set {
			FrameData.Duration = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string SourceFile {
		get => FrameData.Source;
		set {
			FrameData.Source = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(SpriteId));
			OnPropertyChanged(nameof(SpriteDefinition));
		}
	}

	public Guid SpriteId {
		get => FrameData.SpriteId;
		set {
			FrameData.SpriteId = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(SpriteDefinition));
		}
	}

	public Vector2I Origin {
		get => FrameData.Origin;
		set {
			FrameData.Origin = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected ISpriteSource Source => string.IsNullOrWhiteSpace(FrameData.Source)
										  ? new NullSpriteSource()
										  : File.GetSprite(FrameData.Source);

	public IEnumerable<ISpriteDefinition> SpriteDefinitions => Source.Sprites;

	public ISpriteDefinition SpriteDefinition => Source.Sprites.FirstOrDefault(s => s.Id == FrameData.SpriteId)
												 ?? new NullSpriteDefinition();

	protected void Save() => File.Save();

	public void Delete() => Animation.RemoveFrame(this, FrameData);

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