using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects;

public class InfoList<TInfo, TData> : ObservableCollectionInfo<TInfo, TData>
	where TData : class, new() {
	private readonly IFileNavigator _file;
	public InfoList(IFileNavigator file, List<TData> data, Func<TData, TInfo> createInfo) : base(new InfoListFactory<TInfo, TData>(createInfo), data) {
		_file = file;
	}
	protected override void Save() {
		_file.Save();
	}
}

public class InfoListFactory<TInfo, TData> : IInfoFactory<TInfo, TData>
	where TData : class, new() {
	private readonly Func<TData, TInfo> _createInfo;

	public InfoListFactory(Func<TData, TInfo> createInfo) {
		_createInfo = createInfo;
	}

	public TInfo CreateInfo(TData data) => _createInfo(data);

	public TData CreateData() => new();
}