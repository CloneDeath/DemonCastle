using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventInfo : BaseInfo<SceneEventData> {
	public SceneEventInfo(IFileNavigator file, SceneEventData data) : base(file, data) {
		When = new SceneEventConditionInfo(file, data.When);
		Then = new SceneEventActionInfo(file, data.Then);
	}

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneEventConditionInfo When { get; }
	public SceneEventActionInfo Then { get; }
}