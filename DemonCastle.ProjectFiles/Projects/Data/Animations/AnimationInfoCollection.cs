using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class AnimationInfoCollection : ObservableCollectionInfo<IAnimationInfo, AnimationData> {
	private readonly IFileNavigator _file;

	public AnimationInfoCollection(IFileNavigator file, List<AnimationData> animations) : base(new AnimationInfoCollectionFactory(file), animations) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public Guid GetAnimationId(string animationName) => InfoItems.FirstOrDefault(a => a.Name == animationName)?.Id ?? Guid.Empty;
}

public class AnimationInfoCollectionFactory : IInfoFactory<IAnimationInfo, AnimationData> {
	private readonly IFileNavigator _file;

	public AnimationInfoCollectionFactory(IFileNavigator file) {
		_file = file;
	}

	public IAnimationInfo CreateInfo(AnimationData data) => new AnimationInfo(_file, data);

	public AnimationData CreateData() => new();
}