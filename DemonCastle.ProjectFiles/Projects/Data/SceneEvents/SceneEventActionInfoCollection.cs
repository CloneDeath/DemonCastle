using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventActionInfoCollection : ObservableCollectionInfo<SceneEventActionInfo, SceneEventActionData> {
	private readonly IFileNavigator _file;

	public SceneEventActionInfoCollection(IFileNavigator file, List<SceneEventActionData> slots)
		: base(new SceneEventActionInfoFactory(file), slots) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public void TriggerActions(IGameState gameState) {
		foreach (var action in this) {
			action.TriggerAction(gameState);
		}
	}
}

public class SceneEventActionInfoFactory : IInfoFactory<SceneEventActionInfo, SceneEventActionData> {
	private readonly IFileNavigator _file;

	public SceneEventActionInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public SceneEventActionInfo CreateInfo(SceneEventActionData data) => new(_file, data);
	public SceneEventActionData CreateData() => new();
}