using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public interface IEntityStateInfoCollection : IEnumerableInfo<EntityStateInfo> {
	EntityStateInfo? Get(Guid id);
}

public class EntityStateInfoCollection : ObservableCollectionInfo<EntityStateInfo, EntityStateData>, IEntityStateInfoCollection {
	private readonly IFileNavigator _file;
	public EntityStateInfoCollection(IFileNavigator file, IEntityStateInfoRetriever states, List<EntityStateData> data) : base(new EntityStateInfoFactory(file, states), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public EntityStateInfo? Get(Guid id) => this.FirstOrDefault(s => s.Id == id);
}

public class EntityStateInfoFactory : IInfoFactory<EntityStateInfo, EntityStateData> {
	private readonly IFileNavigator _file;
	private readonly IEntityStateInfoRetriever _states;

	public EntityStateInfoFactory(IFileNavigator file, IEntityStateInfoRetriever states) {
		_file = file;
		_states = states;
	}

	public EntityStateInfo CreateInfo(EntityStateData data) => new(_file, _states, data);
	public EntityStateData CreateData() => new();
}

public class NullEntityStateInfoCollection : NullEnumerableInfo<EntityStateInfo>, IEntityStateInfoCollection {
	public EntityStateInfo? Get(Guid id) => null;
}