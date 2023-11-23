using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponFrameInfoCollection : ObservableCollectionInfo<WeaponFrameInfo, WeaponFrameData> {
	private readonly FileNavigator<WeaponFile> _file;
	protected WeaponAnimationInfo Animation { get; }

	public WeaponFrameInfoCollection(FileNavigator<WeaponFile> file, WeaponAnimationInfo animation, List<WeaponFrameData> frames)
		: base(frames) {
		_file = file;
		Animation = animation;
	}

	protected override void Save() => _file.Save();
	protected override WeaponFrameInfo CreateInfo(WeaponFrameData data, int index) => new(Animation, _file, data, index);
}