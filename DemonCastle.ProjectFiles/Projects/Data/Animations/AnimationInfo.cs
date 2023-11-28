using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class AnimationInfo : IAnimationInfo {
	public AnimationInfo(IFileNavigator file, AnimationData animation) {
		File = file;
		Animation = animation;
		Frames = new FrameInfoCollection(file, this, animation.Frames);
	}

	protected IFileNavigator File { get; }
	protected AnimationData Animation { get; }
	public IEnumerableInfo<IFrameInfo> Frames { get; }

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