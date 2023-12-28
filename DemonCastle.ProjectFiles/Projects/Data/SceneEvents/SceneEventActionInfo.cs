using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventActionInfo : BaseInfo<SceneEventActionData> {
	public SceneEventActionInfo(IFileNavigator file, SceneEventActionData data) : base(file, data) { }
}