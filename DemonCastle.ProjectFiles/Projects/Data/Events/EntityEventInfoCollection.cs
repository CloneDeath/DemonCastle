using System.Collections.Generic;
using DemonCastle.Files.Events;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Events;

public interface IEntityEventInfoCollection : IEnumerableInfo<EntityEventInfo> {
}

public class EntityEventInfoCollection : ObservableCollectionInfo<EntityEventInfo, EntityEventData>, IEntityEventInfoCollection {
	private readonly IFileNavigator _file;

	public EntityEventInfoCollection(IFileNavigator file, List<EntityEventData> EntityEvents) : base(new EntityEventInfoCollectionFactory(file), EntityEvents) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class EntityEventInfoCollectionFactory : IInfoFactory<EntityEventInfo, EntityEventData> {
	private readonly IFileNavigator _file;

	public EntityEventInfoCollectionFactory(IFileNavigator file) {
		_file = file;
	}

	public EntityEventInfo CreateInfo(EntityEventData data) => new(_file, data);

	public EntityEventData CreateData() => new();
}

public class NullEntityEventInfoCollection : NullEnumerableInfo<EntityEventInfo>, IEntityEventInfoCollection {
}