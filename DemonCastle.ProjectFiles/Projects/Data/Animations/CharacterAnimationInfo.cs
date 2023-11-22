using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class CharacterAnimationInfo : IAnimationInfo, INotifyPropertyChanged {
	public CharacterAnimationInfo(FileNavigator<CharacterFile> file, CharacterAnimationData characterAnimation) {
		File = file;
		Animation = characterAnimation;
		CharacterFrames = Animation.Frames.Select((f, i) => new CharacterFrameInfo(this, File, f, i)).ToList();
	}

	protected FileNavigator<CharacterFile> File { get; }
	protected CharacterAnimationData Animation { get; }
	public List<CharacterFrameInfo> CharacterFrames { get; }
	public IEnumerable<IFrameInfo> Frames => CharacterFrames;

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
		var frame = new CharacterFrameData {
			Source = previousFrame?.Source ?? string.Empty,
			SpriteId = previousFrame?.SpriteId ?? Guid.Empty,
			Duration = previousFrame?.Duration ?? 1
		};
		Animation.Frames.Add(frame);
		CharacterFrames.Add(new CharacterFrameInfo(this, File, frame, CharacterFrames.Count));
		Save();
	}

	public void RemoveFrame(CharacterFrameInfo frameInfo, CharacterFrameData frameData) {
		Animation.Frames.Remove(frameData);
		CharacterFrames.Remove(frameInfo);
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