using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponAnimationInfo : IAnimationInfo, INotifyPropertyChanged {
	public WeaponAnimationInfo(ISaveFile file, AnimationData animation) {
		File = file;
		Animation = animation;
		WeaponFrames = new WeaponFrameInfoCollection(file, this, animation.Frames);
	}

	protected ISaveFile File { get; }
	protected AnimationData Animation { get; }
	public WeaponFrameInfoCollection  WeaponFrames { get; }
	public IObservableCollection<IFrameInfo> Frames => WeaponFrames;

	public Guid Id => Animation.Id;

	public string Name {
		get => Animation.Name;
		set {
			Animation.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected void Save() => File.Save();

	public void AddFrame() {
		var previousFrame = Animation.Frames.LastOrDefault();
		var frame = new FrameData {
			Source = previousFrame?.Source ?? string.Empty,
			SpriteId = previousFrame?.SpriteId ?? Guid.Empty,
			Duration = previousFrame?.Duration ?? 1,
			Origin = previousFrame?.Origin ?? Vector2I.Zero
		};
		Animation.Frames.Add(frame);
		WeaponFrames.Add(frame);
		Save();
	}

	public void RemoveFrame(WeaponFrameInfo frameInfo, FrameData frameData) {
		Animation.Frames.Remove(frameData);
		WeaponFrames.Remove(frameInfo);
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