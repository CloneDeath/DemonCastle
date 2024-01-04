using DemonCastle.ProjectFiles.Files.Actions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class EntityActionInfo : BaseInfo<EntityActionData> {
	public EntityActionInfo(IFileNavigator file, EntityActionData data) : base(file, data) { }
}