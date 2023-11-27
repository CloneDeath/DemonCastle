using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class AnimationInfo : IAnimationInfo, INotifyPropertyChanged {
	public AnimationInfo(IFileNavigator file, AnimationData animation) {
		File = file;
		Animation = animation;
		WeaponFrames = new FrameInfoCollection(file, this, animation.Frames);
	}

	protected IFileNavigator File { get; }
	protected AnimationData Animation { get; }
	public FrameInfoCollection WeaponFrames { get; }
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
			Origin = new FrameOrigin {
				Offset = previousFrame?.Origin.Offset ?? Vector2I.Zero,
				Anchor = previousFrame?.Origin.Anchor ?? Vector2I.Zero
			}
		};
		Animation.Frames.Add(frame);
		WeaponFrames.Add(frame);
		Save();
	}

	public void RemoveFrame(FrameInfo frameInfo, FrameData frameData) {
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