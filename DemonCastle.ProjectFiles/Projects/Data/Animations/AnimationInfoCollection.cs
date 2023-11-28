using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class AnimationInfoCollection : IEnumerableInfo<IAnimationInfo> {
	protected IFileNavigator File { get; }
	protected List<AnimationData> FileAnimations { get; }
	protected ObservableCollection<IAnimationInfo> Animations { get; }

	public AnimationInfoCollection(IFileNavigator file, List<AnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = new ObservableCollection<IAnimationInfo>(animations.Select(data => new AnimationInfo(file, data)).ToList());
		Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public IAnimationInfo AppendNew() {
		var animationData = new AnimationData {
			Name = "animation"
		};
		FileAnimations.Add(animationData);
		Save();
		var animationInfo = new AnimationInfo(File, animationData);
		Animations.Add(animationInfo);
		return animationInfo;
	}

	public void Remove(IAnimationInfo item) {
		var index = Animations.IndexOf(item);
		FileAnimations.RemoveAt(index);
		Save();
		Animations.RemoveAt(index);
	}

	public void RemoveAt(int animationIndex) {
		FileAnimations.RemoveAt(animationIndex);
		Save();
		Animations.RemoveAt(animationIndex);
	}

	protected void Save() => File.Save();

	public IAnimationInfo this[int index] => Animations[index];

	#region IEnumerable
	public IEnumerator<IAnimationInfo> GetEnumerator() => Animations.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}