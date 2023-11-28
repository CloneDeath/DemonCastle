using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class FrameSlotInfoCollection : ObservableCollectionInfo<IFrameSlotInfo, FrameSlotData> {
	private readonly IFileNavigator _file;

	public FrameSlotInfoCollection(IFileNavigator file, FrameInfo frame, List<FrameSlotData> frames)
		: base(new FrameSlotInfoFactory(file, frame.Slots), frames) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class FrameSlotInfoFactory : IInfoFactory<IFrameSlotInfo, FrameSlotData> {
	private readonly IFileNavigator _file;
	protected readonly IEnumerableInfo<IFrameSlotInfo> _slots;

	public FrameSlotInfoFactory(IFileNavigator file, IEnumerableInfo<IFrameSlotInfo> slots) {
		_file = file;
		_slots = slots;
	}
	public IFrameSlotInfo CreateInfo(FrameSlotData data) => new FrameSlotInfo(_file, data);
	public FrameSlotData CreateData() {
		var previousSlot = _slots.LastOrDefault();
		return new FrameSlotData {
			Name = previousSlot?.Name ?? string.Empty,
			Animation = previousSlot?.Animation ?? string.Empty,
			Position = previousSlot?.Position ?? Vector2I.Zero
		};
	}
}