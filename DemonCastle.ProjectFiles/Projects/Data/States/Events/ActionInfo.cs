using DemonCastle.ProjectFiles.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class ActionInfo : BaseInfo<MonsterStateActionData> {
	public ActionInfo(IFileNavigator file, MonsterStateActionData data) : base(file, data) { }
}