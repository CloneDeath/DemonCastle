using System.Collections.Generic;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class EntityStateTransitionInfoCollection : ObservableCollectionInfo<EntityStateTransitionInfo, EntityStateTransitionData> {
	private readonly IFileNavigator _file;
	public EntityStateTransitionInfoCollection(IFileNavigator file, IEntityStateInfoRetriever states, List<EntityStateTransitionData> data) : base(new EntityStateTransitionInfoFactory(file, states), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class EntityStateTransitionInfoFactory : IInfoFactory<EntityStateTransitionInfo, EntityStateTransitionData> {
	private readonly IFileNavigator _file;
	private readonly IEntityStateInfoRetriever _states;

	public EntityStateTransitionInfoFactory(IFileNavigator file, IEntityStateInfoRetriever states) {
		_file = file;
		_states = states;
	}

	public EntityStateTransitionInfo CreateInfo(EntityStateTransitionData data) => new(_file, _states, data);
	public EntityStateTransitionData CreateData() => new();
}