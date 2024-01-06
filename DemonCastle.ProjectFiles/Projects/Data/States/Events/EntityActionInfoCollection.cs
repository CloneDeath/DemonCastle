using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class EntityActionInfoCollection : ObservableCollectionInfo<EntityActionInfo, EntityActionData> {
	private readonly IFileNavigator _file;

	public EntityActionInfoCollection(IFileNavigator file, List<EntityActionData> actions) : base(new EntityActionInfoFactory(file), actions) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class EntityActionInfoFactory : IInfoFactory<EntityActionInfo, EntityActionData> {
	private readonly IFileNavigator _file;

	public EntityActionInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public EntityActionInfo CreateInfo(EntityActionData data) => new(_file, data);

	public EntityActionData CreateData() => new();
}