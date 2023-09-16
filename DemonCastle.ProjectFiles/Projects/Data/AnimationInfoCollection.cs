using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data; 

public class AnimationInfoCollection : IEnumerable<AnimationInfo> {
	protected FileNavigator<CharacterFile> File { get; }
	protected List<AnimationData> FileAnimations { get; }
	protected List<AnimationInfo> Animations { get; }

	public AnimationInfoCollection(FileNavigator<CharacterFile> file, List<AnimationData> animations) {
		File = file;
		FileAnimations = animations;
		Animations = animations.Select(data => new AnimationInfo(file, data)).ToList();
	}

	public AnimationInfo Add(AnimationData animationData) {
		FileAnimations.Add(animationData);
		var animInfo = new AnimationInfo(File, animationData);
		Animations.Add(animInfo);
		Save();
		return animInfo;
	}

	public IEnumerator<AnimationInfo> GetEnumerator() => Animations.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void RemoveAt(int animationIndex) {
		Animations.RemoveAt(animationIndex);
		FileAnimations.RemoveAt(animationIndex);
		Save();
	}

	protected void Save() => File.Save();

	public AnimationInfo this[int index] => Animations[index];
}