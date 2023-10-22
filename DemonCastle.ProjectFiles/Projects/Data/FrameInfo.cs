using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class FrameInfo : INotifyPropertyChanged {
	public FrameInfo(AnimationInfo animation, FileNavigator<CharacterFile> file, FrameData frameData, int index) {
		Animation = animation;
		File = file;
		FrameData = frameData;
		Index = index;
	}

	protected AnimationInfo Animation { get; }
	protected FileNavigator<CharacterFile> File { get; }
	public string Directory => File.Directory;
	protected FrameData FrameData { get; }
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
		}
	}

	public string SpriteName {
		get => FrameData.Sprite;
		set {
			FrameData.Sprite = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected ISpriteSource Source => string.IsNullOrWhiteSpace(FrameData.Source)
										  ? new NullSpriteSource()
										  : File.GetSprite(FrameData.Source);

	public IEnumerable<ISpriteDefinition> SpriteDefinitions =>
		Source.SpriteNames.Select(s => Source.GetSpriteDefinition(s));

	public SpriteInfoNode Sprite => new(Source.GetSpriteDefinition(FrameData.Sprite));
	public TextureRect TextureRect => new SpriteDefinitionTextureRect(Source.GetSpriteDefinition(FrameData.Sprite));

	protected void Save() => File.Save();

	public void Delete() {
		Animation.RemoveFrame(this, FrameData);
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