using System.Collections.Generic;
using DemonCastle.Files.Events;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventInfoCollection : ObservableCollectionInfo<SceneEventInfo, SceneEventData> {
	private readonly IFileNavigator _file;

	public SceneEventInfoCollection(IFileNavigator file, List<SceneEventData> slots)
		: base(new SceneEventInfoFactory(file), slots) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class SceneEventInfoFactory : IInfoFactory<SceneEventInfo, SceneEventData> {
	private readonly IFileNavigator _file;

	public SceneEventInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public SceneEventInfo CreateInfo(SceneEventData data) => new(_file, data);
	public SceneEventData CreateData() => new() { Name = "Event" };
}