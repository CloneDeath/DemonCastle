using System;
using DemonCastle.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class AnimationInfo : BaseInfo<AnimationData>, IAnimationInfo {
	public AnimationInfo(IFileNavigator file, AnimationData animation) : base(file, animation) {
		Frames = new FrameInfoCollection(file, this, animation.Frames);
	}

	public Guid Id => Data.Id;
	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(ListLabel);
		}
	}

	public IEnumerableInfo<IFrameInfo> Frames { get; }
}