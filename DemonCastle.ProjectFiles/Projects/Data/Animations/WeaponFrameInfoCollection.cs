using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponFrameInfoCollection : ObservableCollectionInfo<WeaponFrameInfo, WeaponFrameData> {
	private readonly FileNavigator<WeaponFile> _file;

	public WeaponFrameInfoCollection(FileNavigator<WeaponFile> file, WeaponAnimationInfo animation, List<WeaponFrameData> frames)
		: base(new WeaponFrameInfoFactory(file, animation), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class WeaponFrameInfoFactory : IInfoFactory<WeaponFrameInfo, WeaponFrameData> {
	private readonly FileNavigator<WeaponFile> _file;
	private readonly WeaponAnimationInfo _animation;

	public WeaponFrameInfoFactory(FileNavigator<WeaponFile> file, WeaponAnimationInfo animation) {
		_file = file;
		_animation = animation;
	}
	public WeaponFrameInfo CreateInfo(WeaponFrameData data, int index) => new(_animation, _file, data, index);
}