using System.Collections.Generic;
using DemonCastle.Files.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.Events;

public interface IEntityEventInfoCollection : IEnumerableInfo<EntityEventInfo> {
	void CheckAndTriggerEvents(IGameState game, IEntityState entity, EventDetails details);
}

public class EntityEventInfoCollection : ObservableCollectionInfo<EntityEventInfo, EntityEventData>, IEntityEventInfoCollection {
	private readonly IFileNavigator _file;

	public EntityEventInfoCollection(IFileNavigator file, List<EntityEventData> EntityEvents) : base(new EntityEventInfoCollectionFactory(file), EntityEvents) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public void CheckAndTriggerEvents(IGameState game, IEntityState entity, EventDetails details) {
		foreach (var @event in this) {
			@event.CheckAndTriggerEvent(game, entity, details);
		}
	}
}

public class EntityEventInfoCollectionFactory : IInfoFactory<EntityEventInfo, EntityEventData> {
	private readonly IFileNavigator _file;

	public EntityEventInfoCollectionFactory(IFileNavigator file) {
		_file = file;
	}

	public EntityEventInfo CreateInfo(EntityEventData data) => new(_file, data);

	public EntityEventData CreateData() => new() {
		Name = "Event"
	};
}

public class NullEntityEventInfoCollection : NullEnumerableInfo<EntityEventInfo>, IEntityEventInfoCollection {
	public void CheckAndTriggerEvents(IGameState game, IEntityState entity, EventDetails details) {}
}