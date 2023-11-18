using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class AnimationInfoCollection : IEnumerable<AnimationInfo>, INotifyCollectionChanged {
	protected FileNavigator<CharacterFile> File { get; }
	protected List<AnimationData> FileAnimations { get; }
	protected ObservableCollection<AnimationInfo> Animations { get; }

	public AnimationInfoCollection(FileNavigator<CharacterFile> file, List<AnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = new ObservableCollection<AnimationInfo>(animations.Select(data => new AnimationInfo(file, data)).ToList());
		Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public AnimationInfo Add(AnimationData animationData) {
		FileAnimations.Add(animationData);
		Save();
		var animInfo = new AnimationInfo(File, animationData);
		Animations.Add(animInfo);
		return animInfo;
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