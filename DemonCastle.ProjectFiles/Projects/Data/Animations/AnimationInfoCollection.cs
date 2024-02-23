using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public interface IAnimationInfoCollection : IEnumerableInfo<IAnimationInfo> {
	Guid GetAnimationId(string animationName);
	IAnimationInfo? Get(Guid id);
}

public class AnimationInfoCollection : ObservableCollectionInfo<IAnimationInfo, AnimationData>, IAnimationInfoCollection {
	private readonly IFileNavigator _file;

	public AnimationInfoCollection(IFileNavigator file, List<AnimationData> animations) : base(new AnimationInfoCollectionFactory(file), animations) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public Guid GetAnimationId(string animationName) => InfoItems.FirstOrDefault(a => a.Name == animationName)?.Id ?? Guid.Empty;
	public IAnimationInfo? Get(Guid id) => InfoItems.FirstOrDefault(a => a.Id == id);
}

public class AnimationInfoCollectionFactory : IInfoFactory<IAnimationInfo, AnimationData> {
	private readonly IFileNavigator _file;

	public AnimationInfoCollectionFactory(IFileNavigator file) {
		_file = file;
	}

	public IAnimationInfo CreateInfo(AnimationData data) => new AnimationInfo(_file, data);

	public AnimationData CreateData() => new();
}

public class NullAnimationInfoCollection : NullEnumerableInfo<IAnimationInfo>, IAnimationInfoCollection {
	public Guid GetAnimationId(string animationName) => Guid.Empty;

	public IAnimationInfo? Get(Guid id) => null;
}