using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class FrameInfoCollection : ObservableCollectionInfo<IFrameInfo, FrameData> {
	private readonly IFileNavigator _file;

	public FrameInfoCollection(IFileNavigator file, IAnimationInfo animation, List<FrameData> frames)
		: base(new FrameInfoFactory(file, animation.Frames), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class FrameInfoFactory : IInfoFactory<IFrameInfo, FrameData> {
	private readonly IFileNavigator _file;
	protected readonly IEnumerableInfo<IFrameInfo> _frames;

	public FrameInfoFactory(IFileNavigator file, IEnumerableInfo<IFrameInfo> frames) {
		_file = file;
		_frames = frames;
	}
	public IFrameInfo CreateInfo(FrameData data) => new FrameInfo(_frames, _file, data);
	public FrameData CreateData() {
		var previousFrame = _frames.LastOrDefault();
		return new FrameData {
			Source = previousFrame?.SourceFile ?? string.Empty,
			SpriteId = previousFrame?.SpriteId ?? Guid.Empty,
			Duration = previousFrame?.Duration ?? 1,
			Origin = new FrameOrigin {
				Offset = previousFrame?.Offset ?? Vector2I.Zero,
				Anchor = previousFrame?.Anchor ?? Vector2I.Zero
			}
		};
	}
}