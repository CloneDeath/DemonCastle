using System;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public interface IEntityStateInfoRetriever {
	public EntityStateInfo? RetrieveEntityStateInfo(Guid stateId);
}