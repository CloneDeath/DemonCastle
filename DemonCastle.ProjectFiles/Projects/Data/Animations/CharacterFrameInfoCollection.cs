using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class CharacterFrameInfoCollection : ObservableCollectionInfo<CharacterFrameInfo, CharacterFrameData> {
	private readonly FileNavigator<CharacterFile> _file;

	public CharacterFrameInfoCollection(FileNavigator<CharacterFile> file, CharacterAnimationInfo animation, List<CharacterFrameData> frames)
		: base(new CharacterFrameInfoFactory(file, animation), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class CharacterFrameInfoFactory : IInfoFactory<CharacterFrameInfo, CharacterFrameData> {
	private readonly FileNavigator<CharacterFile> _file;
	private readonly CharacterAnimationInfo _animation;

	public CharacterFrameInfoFactory(FileNavigator<CharacterFile> file, CharacterAnimationInfo animation) {
		_file = file;
		_animation = animation;
	}

	public CharacterFrameInfo CreateInfo(CharacterFrameData data, int index) => new(_animation, _file, data, index);
}