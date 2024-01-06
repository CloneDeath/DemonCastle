using System.Collections.Generic;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public class EntityStateInfoCollection : ObservableCollectionInfo<EntityStateInfo, EntityStateData> {
	private readonly IFileNavigator _file;
	public EntityStateInfoCollection(IFileNavigator file, List<EntityStateData> data) : base(new EntityStateInfoFactory(file), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class EntityStateInfoFactory : IInfoFactory<EntityStateInfo, EntityStateData> {
	private readonly IFileNavigator _file;

	public EntityStateInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public EntityStateInfo CreateInfo(EntityStateData data) => new(_file, data);
	public EntityStateData CreateData() => new();
}