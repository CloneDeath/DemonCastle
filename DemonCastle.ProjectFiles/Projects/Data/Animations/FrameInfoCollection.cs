using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class FrameInfoCollection : ObservableCollectionInfo<FrameInfo, FrameData> {
	private readonly IFileNavigator _file;

	public FrameInfoCollection(IFileNavigator file, AnimationInfo animation, List<FrameData> frames)
		: base(new WeaponFrameInfoFactory(file, animation), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class WeaponFrameInfoFactory : IInfoFactory<FrameInfo, FrameData> {
	private readonly IFileNavigator _file;
	protected readonly AnimationInfo _animation;

	public WeaponFrameInfoFactory(IFileNavigator file, AnimationInfo animation) {
		_file = file;
		_animation = animation;
	}
	public FrameInfo CreateInfo(FrameData data, int index) => new(_animation, _file, data, index);
}