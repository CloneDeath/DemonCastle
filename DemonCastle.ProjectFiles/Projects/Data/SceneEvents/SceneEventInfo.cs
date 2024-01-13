using DemonCastle.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventInfo : BaseInfo<SceneEventData>, IListableInfo {
	public SceneEventInfo(IFileNavigator file, SceneEventData data) : base(file, data) {
		When = new SceneEventConditionInfo(file, data.When);
		Then = new SceneEventActionInfoCollection(file, data.Then);
	}

	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public SceneEventConditionInfo When { get; }
	public SceneEventActionInfoCollection Then { get; }

	public void TriggerEvent(IGameState gameState, SceneState sceneState) {
		if (When.IsConditionMet(gameState, sceneState)) {
			Then.TriggerActions(gameState);
		}
	}
}