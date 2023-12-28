using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventInfo : BaseInfo<SceneEventData> {
	public SceneEventInfo(IFileNavigator file, SceneEventData data) : base(file, data) { }

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}
}