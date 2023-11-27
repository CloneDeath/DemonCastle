using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponFrameInfoCollection : ObservableCollectionInfo<WeaponFrameInfo, FrameData> {
	private readonly IFileNavigator _file;

	public WeaponFrameInfoCollection(IFileNavigator file, WeaponAnimationInfo animation, List<FrameData> frames)
		: base(new WeaponFrameInfoFactory(file, animation), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class WeaponFrameInfoFactory : IInfoFactory<WeaponFrameInfo, FrameData> {
	private readonly IFileNavigator _file;
	protected readonly WeaponAnimationInfo _animation;

	public WeaponFrameInfoFactory(IFileNavigator file, WeaponAnimationInfo animation) {
		_file = file;
		_animation = animation;
	}
	public WeaponFrameInfo CreateInfo(FrameData data, int index) => new(_animation, _file, data, index);
}