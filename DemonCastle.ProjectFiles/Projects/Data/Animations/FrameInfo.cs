using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo : INotifyPropertyChanged {
	float Duration { get; set; }
	Vector2I Anchor { get; set; }
	Vector2I Offset { get; set; }
	Vector2I Origin { get; }

	string SourceFile { get; set; }
	Guid SpriteId { get; set; }
	ISpriteDefinition SpriteDefinition { get; }
	public IEnumerable<ISpriteDefinition> SpriteDefinitions { get; }

	IEnumerableInfo<IFrameSlotInfo> Slots { get; }

	void Delete();
}

public class FrameInfo : IFrameInfo {
	public FrameInfo(IEnumerableInfo<IFrameInfo> frames, IFileNavigator file, FrameData data) {
		Frames = frames;
		File = file;
		FrameData = data;
		Slots = new FrameSlotInfoCollection(file, data.Slots);
	}

	protected IEnumerableInfo<IFrameInfo> Frames { get; }
	protected IFileNavigator File { get; }
	protected FrameData FrameData { get; }

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

	public Vector2I Anchor {
		get => FrameData.Origin.Anchor;
		set {
			FrameData.Origin.Anchor = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Origin));
		}
	}

	public Vector2I Offset {
		get => FrameData.Origin.Offset;
		set {
			FrameData.Origin.Offset = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Origin));
		}
	}

	public Vector2I Origin => SpriteDefinition.Region.Size * (Anchor + Vector2I.One) / 2 + Offset;

	protected ISpriteSource SpriteSource => string.IsNullOrWhiteSpace(FrameData.Source)
												? new NullSpriteSource()
												: File.GetSprite(FrameData.Source);

	public IEnumerable<ISpriteDefinition> SpriteDefinitions => SpriteSource.Sprites;

	public ISpriteDefinition SpriteDefinition => SpriteSource.Sprites.FirstOrDefault(s => s.Id == FrameData.SpriteId)
												 ?? new NullSpriteDefinition();

	public IEnumerableInfo<IFrameSlotInfo> Slots { get; }


	protected void Save() => File.Save();

	public void Delete() => Frames.Remove(this);

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