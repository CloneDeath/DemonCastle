using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;

public class MonsterDataInfoCollection : ObservableCollectionInfo<MonsterDataInfo, MonsterData> {
	private readonly IFileNavigator _file;
	public MonsterDataInfoCollection(IFileNavigator file, AreaInfo area, List<MonsterData> data) : base(new MonsterDataInfoFactory(file, area), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class MonsterDataInfoFactory : IInfoFactory<MonsterDataInfo, MonsterData> {
	private readonly IFileNavigator _file;
	private readonly AreaInfo _area;

	public MonsterDataInfoFactory(IFileNavigator file, AreaInfo area) {
		_area = area;
		_file = file;
	}
	public MonsterDataInfo CreateInfo(MonsterData data) => new(_file, _area, data);
	public MonsterData CreateData() {
		return new MonsterData();
	}
}