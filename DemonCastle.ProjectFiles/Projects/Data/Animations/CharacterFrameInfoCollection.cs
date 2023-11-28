using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class CharacterFrameInfoCollection : ObservableCollectionInfo<CharacterFrameInfo, CharacterFrameData> {
	private readonly FileNavigator<CharacterFile> _file;

	public CharacterFrameInfoCollection(FileNavigator<CharacterFile> file, IAnimationInfo animation, List<CharacterFrameData> frames)
		: base(new CharacterFrameInfoFactory(file, animation.Frames), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class CharacterFrameInfoFactory : IInfoFactory<CharacterFrameInfo, CharacterFrameData> {
	private readonly FileNavigator<CharacterFile> _file;
	private readonly IEnumerableInfo<IFrameInfo> _frames;

	public CharacterFrameInfoFactory(FileNavigator<CharacterFile> file, IEnumerableInfo<IFrameInfo> frames) {
		_file = file;
		_frames = frames;
	}

	public CharacterFrameInfo CreateInfo(CharacterFrameData data) => new(_frames, _file, data);
	public CharacterFrameData CreateData() {
		var previousFrame = _frames.LastOrDefault();
		return new CharacterFrameData {
			Source = previousFrame?.SourceFile ?? string.Empty,
			SpriteId = previousFrame?.SpriteId ?? Guid.Empty,
			Duration = previousFrame?.Duration ?? 1,
			Origin = new FrameOrigin {
				Anchor = previousFrame?.Anchor ?? Vector2I.Down,
				Offset = previousFrame?.Offset ?? Vector2I.Zero
			}
		};
	}
}