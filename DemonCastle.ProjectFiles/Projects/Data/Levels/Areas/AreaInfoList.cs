using System.Collections.Generic;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;

public class AreaInfoList : ObservableCollectionInfo<AreaInfo, AreaData>{
	private readonly IFileNavigator _file;
	public AreaInfoList(IFileNavigator file, List<AreaData> data, LevelInfo level) : base(new AreaInfoListFactory(file, level), data) {
		_file = file;
	}
	protected override void Save() {
		_file.Save();
	}
}

public class AreaInfoListFactory : IInfoFactory<AreaInfo, AreaData> {
	private readonly IFileNavigator _file;
	private readonly LevelInfo _level;

	public AreaInfoListFactory(IFileNavigator file, LevelInfo level) {
		_file = file;
		_level = level;
	}

	public AreaInfo CreateInfo(AreaData data) => new AreaInfo(_file, data, _level);

	public AreaData CreateData() => new();
}