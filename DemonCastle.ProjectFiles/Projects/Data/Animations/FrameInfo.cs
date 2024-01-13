using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Files.Animations;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IFrameInfo : INotifyPropertyChanged {
	float Duration { get; set; }
	Vector2I Anchor { get; set; }
	Vector2I Offset { get; set; }
	Vector2I Origin { get; }

	Rect2I? HitBox { get; set; }
	Rect2I? HurtBox { get; set; }

	string? Audio { get; set; }
	AudioStream? AudioStream { get; }

	string SourceFile { get; set; }
	Guid SpriteId { get; set; }
	ISpriteDefinition SpriteDefinition { get; }
	public IEnumerable<ISpriteDefinition> SpriteDefinitions { get; }

	IEnumerableInfo<IFrameSlotInfo> Slots { get; }

	void Delete();
}

public class FrameInfo : BaseInfo<FrameData>, IFrameInfo {
	protected IAnimationInfo Animation { get; }

	public FrameInfo(IAnimationInfo animation, IFileNavigator file, FrameData data) : base(file, data) {
		Animation = animation;
		Slots = new FrameSlotInfoCollection(file, data.Slots);
	}

	protected IEnumerableInfo<IFrameInfo> Frames => Animation.Frames;

	public float Duration {
		get => Data.Duration;
		set {
			Data.Duration = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string SourceFile {
		get => Data.Source;
		set {
			Data.Source = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(SpriteId));
			OnPropertyChanged(nameof(SpriteDefinition));
		}
	}

	public Guid SpriteId {
		get => Data.SpriteId;
		set {
			Data.SpriteId = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(SpriteDefinition));
		}
	}

	public Vector2I Anchor {
		get => Data.Origin.Anchor;
		set {
			Data.Origin.Anchor = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Origin));
		}
	}

	public Vector2I Offset {
		get => Data.Origin.Offset;
		set {
			Data.Origin.Offset = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Origin));
		}
	}

	public Rect2I? HitBox {
		get => Data.HitBox?.ToRect2I();
		set {
			Data.HitBox = value?.ToRegion2I();
			Save();
			OnPropertyChanged();
		}
	}

	public Rect2I? HurtBox {
		get => Data.HurtBox?.ToRect2I();
		set {
			Data.HurtBox = value?.ToRegion2I();
			Save();
			OnPropertyChanged();
		}
	}

	public string? Audio {
		get => Data.Audio;
		set {
			Data.Audio = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(AudioStream));
		}
	}


	public AudioStream? AudioStream => Data.Audio != null && File.FileExists(Data.Audio) ? File.GetAudioStream(Data.Audio) : null;

	public Vector2I Origin => (SpriteDefinition.Region.Size - Vector2I.One) * (Anchor + Vector2I.One) / 2 + Offset;

	protected ISpriteSource SpriteSource => File.FileExists(Data.Source)
												? File.GetSprite(Data.Source)
												: new NullSpriteSource();

	public IEnumerable<ISpriteDefinition> SpriteDefinitions => SpriteSource.Sprites;

	public ISpriteDefinition SpriteDefinition => SpriteSource.Sprites.FirstOrDefault(s => s.Id == Data.SpriteId)
												 ?? new NullSpriteDefinition();

	public IEnumerableInfo<IFrameSlotInfo> Slots { get; }

	public void Delete() => Frames.Remove(this);
}