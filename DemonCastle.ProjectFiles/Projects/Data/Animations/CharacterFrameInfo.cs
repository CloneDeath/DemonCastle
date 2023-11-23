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

public class CharacterFrameInfo : IFrameInfo {
	public CharacterFrameInfo(CharacterAnimationInfo animation, FileNavigator<CharacterFile> file, CharacterFrameData frameData, int index) {
		Animation = animation;
		File = file;
		FrameData = frameData;
		Index = index;
	}

	protected CharacterAnimationInfo Animation { get; }
	protected FileNavigator<CharacterFile> File { get; }
	public string Directory => File.Directory;
	protected CharacterFrameData FrameData { get; }
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

	public bool WeaponEnabled {
		get => FrameData.Weapon.Enabled;
		set {
			FrameData.Weapon.Enabled = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string WeaponAnimation {
		get => FrameData.Weapon.Animation;
		set {
			FrameData.Weapon.Animation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I WeaponPosition {
		get => FrameData.Weapon.Position;
		set {
			FrameData.Weapon.Position = value;
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
	public void Delete() => Animation.RemoveFrame(this);

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