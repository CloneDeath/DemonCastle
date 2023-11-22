using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponAnimationInfoCollection : IEnumerable<WeaponAnimationInfo>, INotifyCollectionChanged {
	protected FileNavigator<WeaponFile> File { get; }
	protected List<WeaponAnimationData> FileAnimations { get; }
	protected ObservableCollection<WeaponAnimationInfo> Animations { get; }

	public WeaponAnimationInfoCollection(FileNavigator<WeaponFile> file, List<WeaponAnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = new ObservableCollection<WeaponAnimationInfo>(animations.Select(data => new WeaponAnimationInfo(file, data)).ToList());
		Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public WeaponAnimationInfo Add(WeaponAnimationData animationData) {
		FileAnimations.Add(animationData);
		Save();
		var animationInfo = new WeaponAnimationInfo(File, animationData);
		Animations.Add(animationInfo);
		return animationInfo;
	}

	public void RemoveAt(int animationIndex) {
		FileAnimations.RemoveAt(animationIndex);
		Save();
		Animations.RemoveAt(animationIndex);
	}

	protected void Save() => File.Save();

	public WeaponAnimationInfo this[int index] => Animations[index];

	#region IEnumerable
	public IEnumerator<WeaponAnimationInfo> GetEnumerator() => Animations.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}