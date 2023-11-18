using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class AnimationInfo : INotifyPropertyChanged {
	public AnimationInfo(FileNavigator<CharacterFile> file, AnimationData animation) {
		File = file;
		Animation = animation;
		Frames = Animation.Frames.Select((f, i) => new FrameInfo(this, File, f, i)).ToList();
	}

	protected FileNavigator<CharacterFile> File { get; }
	protected AnimationData Animation { get; }
	public List<FrameInfo> Frames { get; }

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
			Duration = previousFrame?.Duration ?? 1
		};
		Animation.Frames.Add(frame);
		Frames.Add(new FrameInfo(this, File, frame, Frames.Count));
		Save();
	}

	public void RemoveFrame(FrameInfo frameInfo, FrameData frameData) {
		Animation.Frames.Remove(frameData);
		Frames.Remove(frameInfo);
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