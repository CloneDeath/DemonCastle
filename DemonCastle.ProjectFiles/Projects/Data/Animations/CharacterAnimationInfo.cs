using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class CharacterAnimationInfo : IAnimationInfo, INotifyPropertyChanged {
	public CharacterAnimationInfo(FileNavigator<CharacterFile> file, CharacterAnimationData animation) {
		File = file;
		Animation = animation;
		CharacterFrames = new CharacterFrameInfoCollection(file, this, animation.Frames);
	}

	protected FileNavigator<CharacterFile> File { get; }
	protected CharacterAnimationData Animation { get; }
	public CharacterFrameInfoCollection CharacterFrames { get; }
	public IObservableCollection<IFrameInfo> Frames => CharacterFrames;

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
		CharacterFrames.Add(frame);
		OnPropertyChanged(nameof(CharacterFrames));
		OnPropertyChanged(nameof(Frames));
	}

	public void RemoveFrame(CharacterFrameInfo frameInfo) {
		CharacterFrames.Remove(frameInfo);
		OnPropertyChanged(nameof(CharacterFrames));
		OnPropertyChanged(nameof(Frames));
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