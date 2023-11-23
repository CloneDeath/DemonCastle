using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class CharacterFrameInfoCollection : ObservableCollectionInfo<CharacterFrameInfo, CharacterFrameData> {
	private readonly FileNavigator<CharacterFile> _file;
	protected CharacterAnimationInfo Animation { get; }

	public CharacterFrameInfoCollection(FileNavigator<CharacterFile> file, CharacterAnimationInfo animation, List<CharacterFrameData> frames)
		: base(frames) {
		_file = file;
		Animation = animation;
	}

	protected override void Save() => _file.Save();
	protected override CharacterFrameInfo CreateInfo(CharacterFrameData data, int index) => new(Animation, _file, data, index);
}