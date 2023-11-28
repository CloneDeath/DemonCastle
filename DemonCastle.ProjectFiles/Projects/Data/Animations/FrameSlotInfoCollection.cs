using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class FrameSlotInfoCollection : ObservableCollectionInfo<IFrameSlotInfo, FrameSlotData> {
	private readonly IFileNavigator _file;

	public FrameSlotInfoCollection(IFileNavigator file, List<FrameSlotData> slots)
		: base(new FrameSlotInfoFactory(file), slots) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class FrameSlotInfoFactory : IInfoFactory<IFrameSlotInfo, FrameSlotData> {
	private readonly IFileNavigator _file;

	public FrameSlotInfoFactory(IFileNavigator file) {
		_file = file;
	}
	public IFrameSlotInfo CreateInfo(FrameSlotData data) => new FrameSlotInfo(_file, data);
	public FrameSlotData CreateData() {
		return new FrameSlotData();
	}
}