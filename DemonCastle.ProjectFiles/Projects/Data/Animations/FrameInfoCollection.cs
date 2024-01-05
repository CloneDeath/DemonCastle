using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class FrameInfoCollection : ObservableCollectionInfo<IFrameInfo, FrameData> {
	private readonly IFileNavigator _file;

	public FrameInfoCollection(IFileNavigator file, IAnimationInfo animation, List<FrameData> frames)
		: base(new FrameInfoFactory(file, animation), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class FrameInfoFactory : IInfoFactory<IFrameInfo, FrameData> {
	private readonly IFileNavigator _file;
	private readonly IAnimationInfo _animation;

	public FrameInfoFactory(IFileNavigator file, IAnimationInfo animation) {
		_file = file;
		_animation = animation;
	}
	public IFrameInfo CreateInfo(FrameData data) => new FrameInfo(_animation, _file, data);
	public FrameData CreateData() {
		var previousFrame = _animation.Frames.LastOrDefault();
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