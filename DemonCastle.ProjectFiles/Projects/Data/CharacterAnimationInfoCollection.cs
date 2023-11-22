using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class CharacterAnimationInfoCollection : IEnumerable<CharacterAnimationInfo>, INotifyCollectionChanged {
	protected FileNavigator<CharacterFile> File { get; }
	protected List<CharacterAnimationData> FileAnimations { get; }
	protected ObservableCollection<CharacterAnimationInfo> Animations { get; }

	public CharacterAnimationInfoCollection(FileNavigator<CharacterFile> file, List<CharacterAnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = new ObservableCollection<CharacterAnimationInfo>(animations.Select(data => new CharacterAnimationInfo(file, data)).ToList());
		Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public CharacterAnimationInfo Add(CharacterAnimationData characterAnimationData) {
		FileAnimations.Add(characterAnimationData);
		Save();
		var animInfo = new CharacterAnimationInfo(File, characterAnimationData);
		Animations.Add(animInfo);
		return animInfo;
	}

	public void RemoveAt(int animationIndex) {
		FileAnimations.RemoveAt(animationIndex);
		Save();
		Animations.RemoveAt(animationIndex);
	}

	protected void Save() => File.Save();

	public CharacterAnimationInfo this[int index] => Animations[index];

	#region IEnumerable
	public IEnumerator<CharacterAnimationInfo> GetEnumerator() => Animations.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}