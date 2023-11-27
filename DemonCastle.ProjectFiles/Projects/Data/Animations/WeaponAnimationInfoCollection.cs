using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponAnimationInfoCollection : IEnumerable<AnimationInfo>, INotifyCollectionChanged {
	protected FileNavigator<WeaponFile> File { get; }
	protected List<AnimationData> FileAnimations { get; }
	protected ObservableCollection<AnimationInfo> Animations { get; }

	public WeaponAnimationInfoCollection(FileNavigator<WeaponFile> file, List<AnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = new ObservableCollection<AnimationInfo>(animations.Select(data => new AnimationInfo(file, data)).ToList());
		Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public AnimationInfo Add(AnimationData animationData) {
		FileAnimations.Add(animationData);
		Save();
		var animationInfo = new AnimationInfo(File, animationData);
		Animations.Add(animationInfo);
		return animationInfo;
	}

	public void RemoveAt(int animationIndex) {
		FileAnimations.RemoveAt(animationIndex);
		Save();
		Animations.RemoveAt(animationIndex);
	}

	protected void Save() => File.Save();

	public AnimationInfo this[int index] => Animations[index];

	#region IEnumerable
	public IEnumerator<AnimationInfo> GetEnumerator() => Animations.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}