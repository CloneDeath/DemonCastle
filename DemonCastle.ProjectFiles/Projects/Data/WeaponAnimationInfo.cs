using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class WeaponAnimationInfo : INotifyPropertyChanged {
	public WeaponAnimationInfo(FileNavigator<WeaponFile> file, WeaponAnimationData animation) {
		File = file;
		Animation = animation;
		Frames = Animation.Frames.Select((f, i) => new WeaponFrameInfo(this, File, f, i)).ToList();
	}

	protected FileNavigator<WeaponFile> File { get; }
	protected WeaponAnimationData Animation { get; }
	public List<WeaponFrameInfo> Frames { get; }

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
		var frame = new WeaponFrameData {
			Source = previousFrame?.Source ?? string.Empty,
			SpriteId = previousFrame?.SpriteId ?? Guid.Empty,
			Duration = previousFrame?.Duration ?? 1,
			Origin = previousFrame?.Origin ?? Vector2I.Zero
		};
		Animation.Frames.Add(frame);
		Frames.Add(new WeaponFrameInfo(this, File, frame, Frames.Count));
		Save();
	}

	public void RemoveFrame(WeaponFrameInfo frameInfo, WeaponFrameData frameData) {
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